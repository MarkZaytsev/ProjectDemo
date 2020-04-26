using Common.FrostLib.Signals.impl;
using UnityEngine;

namespace Common.FrostLib.Containers
{
    public class LimitedFloatValueContainer
    {
        public float Value => _valueContainer.Value;
        public float MaxValue => _maxContainer.Value;

        public Signal<float, float> OnValueChangedSignal => _valueContainer.OnValueChangedSignal;
        public Signal<float, float> OnNewMaxValueChangedSignal => _maxContainer.OnValueChangedSignal;

        private readonly FloatContainer _valueContainer;
        private readonly FloatContainer _maxContainer;

        public LimitedFloatValueContainer(float value, float maxValue)
        {
            _valueContainer = new FloatContainer(value);
            _maxContainer = new FloatContainer(maxValue);
        }

        public void Modify(float value) => Set(Value + value);

        public void Set(float newValue) => _valueContainer.Set(Mathf.Clamp(newValue, 0, MaxValue));

        public void ModifyMax(float value) => SetMax(_maxContainer.Value + value);

        public void SetMax(float newValue) =>
            _maxContainer.Set(Mathf.Max(newValue, 0));
    }
}