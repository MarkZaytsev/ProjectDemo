using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Modules
{
    [CreateAssetMenu(fileName = "FireRatePercentage", menuName = "Ships/Modules/FireRatePercentage", order = 2)]
    public class FireRatePercentageModule : Module
    {
        [Tooltip("%")]
        [SerializeField]
        private float _reloadSpeedBoost;

        public override void Apply(AspectedEntity entity) => 
            entity.GetAspect<GunsAspect>().ScaleReloadTime(1f + _reloadSpeedBoost / 100f);
    }
}