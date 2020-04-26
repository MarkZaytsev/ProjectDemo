using System;
using System.Collections.Generic;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using Common.FrostLib.Signals.impl;

namespace ShipsSection.Scripts.Aspects
{
    public class TakeDamageAspect : Aspect, IDisposable
    {
        public readonly Signal<float, AspectedEntity> OnDamageTakenSignal = new Signal<float, AspectedEntity>();

        [AspectDependency]
        public IdentityAspect Identity { get; private set; }
        [AspectDependency]
        private HealthAspect Health { get; set; }

        private readonly List<IDamageHandler> _handlers = new List<IDamageHandler>();

        public void AddHandler(IDamageHandler handler)
        {
            if (handler == null)
                return;

            _handlers.Add(handler);
        }

        public void Take(float damage, AspectedEntity source)
        {
            damage = ApplyHandlers(damage);
            if (damage <= 0f)
                return;

            Health.Modify(-damage);
            OnDamageTakenSignal.Dispatch(damage, source);
        }

        private float ApplyHandlers(float damage)
        {
            foreach (var handler in _handlers)
                damage = handler.Handle(damage);

            return damage;
        }

        public void Dispose() => OnDamageTakenSignal.ClearListeners();
    }
}