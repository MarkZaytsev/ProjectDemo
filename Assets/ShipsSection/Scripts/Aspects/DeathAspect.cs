using System;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using Common.FrostLib.Signals.impl;

namespace ShipsSection.Scripts.Aspects
{
    public class DeathAspect : Aspect, IDisposable
    {
        public readonly Signal OnTriggerSignal = new Signal();

        [AspectDependency]
        private HealthAspect Health { get; set; }

        public override void OnDependencyResolved()
        {
            base.OnDependencyResolved();
            Health.OnValueChangedSignal.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(float health, float diff = 0)
        {
            if (health > 0f)
                return;

            OnTriggerSignal.Dispatch();
        }

        public void Dispose()
        {
            Health.OnValueChangedSignal.RemoveListener(OnHealthChanged);
            OnTriggerSignal.ClearListeners();
        }
    }
}