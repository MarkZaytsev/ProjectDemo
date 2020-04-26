using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using ShipsSection.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipsSection.Scripts.Aspects
{
    public partial class GunsAspect : Aspect, IDisposable
    {
        [AspectDependency]
        private DealDamageToTargetsAspect DamageAspects { get; set; }

        private readonly Gun[] _guns;

        public GunsAspect(IReadOnlyList<GunData> gunsData)
        {
            _guns = new Gun[gunsData.Count];
            SetupGuns(gunsData);
        }

        private void SetupGuns(IReadOnlyList<GunData> gunsData)
        {
            for (var i = 0; i < gunsData.Count; i++)
            {
                var data = gunsData[i];
                var gun = new Gun(data);
                gun.OnFireSignal.AddListener(DealDamage);
                _guns[i] = gun;
            }
        }

        private void DealDamage(float value) => DamageAspects.Deal(value);

        public void Fire()
        {
            foreach (var gun in _guns)
                gun.Fire();
        }

        public void Hold()
        {
            foreach (var gun in _guns)
                gun.Hold();
        }

        public void ScaleReloadTime(float scale)
        {
            foreach (var gun in _guns)
                gun.ScaleReloadTime(scale);
        }

        public GunInfo[] GetGunsInfo() =>
            _guns.Select(g => new GunInfo(g.Name, g.OnReloadStartedSignal)).ToArray();

        public void Dispose()
        {
            foreach (var gun in _guns)
            {
                gun.OnFireSignal.RemoveListener(DamageAspects.Deal);
                gun.Dispose();
            }
        }
    }
}