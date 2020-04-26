using Common.FrostLib.Aspects;
using Common.FrostLib.Containers;
using Common.FrostLib.Signals.impl;
using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public class ResourceAspect : Aspect
    {
        public float Current => Container.Value;
        public float Max => Container.MaxValue;

        public Signal<float, float> OnValueChangedSignal => Container.OnValueChangedSignal;

        protected readonly LimitedFloatValueContainer Container;

        public ResourceAspect(float defaultValue) =>
            Container = new LimitedFloatValueContainer(defaultValue, defaultValue);

        public void Set(float newValue) => Container.Set(newValue);

        public void ModifyMax(float value) => Container.ModifyMax(value);

        public void Modify(float value) => Container.Modify(value);

        public void ApplyBoost(float boost)
        {
            ModifyMax(boost);
            Set(Max);
        }
    }
}