using System;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public class LogDealDamageAspect : Aspect, IDisposable
    {
        [AspectDependency]
        private IdentityAspect Identity { get; set; }
        [AspectDependency]
        private DealDamageToTargetsAspect Dealer { get; set; }

        public override void OnDependencyResolved()
        {
            base.OnDependencyResolved();
            Dealer.OnPrepareDealDamageSignal.AddListener(Handle);
        }

        private void Handle(float value, IdentityAspect target) => 
            Debug.Log($"{Identity} -> {target} [{value} damage]");

        public void Dispose() => Dealer.OnPrepareDealDamageSignal.RemoveListener(Handle);
    }
}