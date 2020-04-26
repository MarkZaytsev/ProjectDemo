using System;
using System.Collections;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using Common.FrostLib.Coroutines;
using Common.FrostLib.Services;
using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public class RechargeShieldAspect : Aspect, IDisposable
    {
        [AspectDependency]
        private ShieldAspect Shield { get; set; }

        private static ServiceLocator Servicer => ServiceLocator.Instance;
        private static IRoutineRunner Routiner => Servicer.Get<IRoutineRunner>();

        private float _rate;
        private bool _active = true;

        public RechargeShieldAspect(float rate) => _rate = rate;

        public void Scale(float scale) => _rate *= scale;

        public override void OnDependencyResolved() => Routiner.StartRoutine(Recharge());

        private IEnumerator Recharge()
        {
            var wait = new WaitForSeconds(1f);
            while (_active)
            {
                Shield.Modify(_rate);
                yield return wait;
            }
        }

        public void Dispose() => _active = false;
    }
}