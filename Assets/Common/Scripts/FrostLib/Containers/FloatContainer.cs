using Common.FrostLib.Signals.impl;
using UnityEngine;

namespace Common.FrostLib.Containers
{
    public class FloatContainer
    {
        public float Value { get; protected set; }
        public readonly Signal<float, float> OnValueChangedSignal = new Signal<float, float>();

        public FloatContainer(float value) => Value = value;

        public void Modify(float value) => Set(Value + value);

        public void Set(float newValue)
        {
            var oldValue = Value;
            Value = newValue;

            var diff = Value - oldValue;
            if (Mathf.Approximately(diff, 0f))
                return;

            Notify(diff);
        }

        private void Notify(float diff) => OnValueChangedSignal.Dispatch(Value, diff);
    }
}