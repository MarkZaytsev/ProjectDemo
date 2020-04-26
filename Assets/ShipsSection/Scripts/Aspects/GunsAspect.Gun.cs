using System;
using System.Collections;
using Common.FrostLib.Coroutines;
using Common.FrostLib.Services;
using Common.FrostLib.Signals.impl;
using ShipsSection.Scripts.Data;
using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public partial class GunsAspect
    {
        private class Gun : IDisposable
        {
            public string Name => _data.Name;

            public readonly Signal<float> OnFireSignal = new Signal<float>();
            public readonly Signal<float> OnReloadStartedSignal = new Signal<float>();

            private static ServiceLocator Servicer => ServiceLocator.Instance;
            private static IRoutineRunner Routiner => Servicer.Get<IRoutineRunner>();

            private readonly GunData _data;

            private float _damage;
            private float _reloadTime;

            private bool _active;
            private bool _fireFnished = true;

            public Gun(GunData data)
            {
                _data = data;

                _damage = _data.Damage;
                _reloadTime = _data.ReloadTime;
            }

            public void Fire()
            {
                _active = true;

                if(_fireFnished)
                    Routiner.StartRoutine(Countdown());
            }

            public void Hold() => _active = false;

            //TODO: handle high time scale. Tick miss is possible with a time scale 100+.
            private IEnumerator Countdown()
            {
                while (_active)
                {
                    OnFireSignal.Dispatch(_damage);
                    OnReloadStartedSignal.Dispatch(_reloadTime);
                    yield return new WaitForSeconds(_reloadTime);
                }

                _fireFnished = true;
            }

            public void ScaleReloadTime(float scale) => _reloadTime *= scale;

            public void Dispose() => Hold();
        }
    }
}