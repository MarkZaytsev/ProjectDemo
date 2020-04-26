using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Modules
{
    public abstract class FlatBonusModule<T> : Module where T : ResourceAspect
    {
        [SerializeField]
        private int _value;

        public override void Apply(AspectedEntity entity) => entity?.GetAspect<T>().ApplyBoost(_value);
    }
}