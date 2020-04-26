using System.Collections.Generic;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using Common.FrostLib.Signals.impl;

namespace ShipsSection.Scripts.Aspects
{
    public class DealDamageToTargetsAspect : Aspect
    {
        public readonly Signal<float, IdentityAspect> OnPrepareDealDamageSignal = new Signal<float, IdentityAspect>();

        [EntityDependency]
        private AspectedEntity Entity { get; set; }

        private readonly List<TakeDamageAspect> _targets = new List<TakeDamageAspect>();

        public void AddTarget(TakeDamageAspect target) => _targets.Add(target);

        public void RemoveTarget(TakeDamageAspect target) => _targets.Remove(target);

        public void Deal(float value)
        {
            for (var i = _targets.Count - 1; i >= 0; i--)
            {
                var asp = _targets[i];
                OnPrepareDealDamageSignal.Dispatch(value, asp.Identity);
                asp.Take(value, Entity);
            }
        }

        public void AddTargets(IEnumerable<TakeDamageAspect> targets)
        {
            foreach (var target in targets)
                AddTarget(target);
        }
    }
}