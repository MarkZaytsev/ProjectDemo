using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Modules
{
    [CreateAssetMenu(fileName = "ShieldRechargePercentage", menuName = "Ships/Modules/ShieldRechargePercentage", order = 3)]
    public class ShieldRechargePercentageModule : Module
    {
        [Tooltip("%")]
        [SerializeField]
        private float _rechargeRateScale;

        public override void Apply(AspectedEntity entity) => 
            entity?.GetAspect<RechargeShieldAspect>().Scale(1f + _rechargeRateScale / 100f);
    }
}