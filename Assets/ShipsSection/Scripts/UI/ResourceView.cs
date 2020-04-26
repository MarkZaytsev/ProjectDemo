using System;
using ShipsSection.Scripts.Aspects;
using UnityEngine;
using UnityEngine.UI;

namespace ShipsSection.Scripts.UI
{
    [Serializable]
    public class ResourceView : IDisposable
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Text _counter;

        private ResourceAspect _asp;

        public void Connect(ResourceAspect asp)
        {
            Disconnect();

            _asp = asp;

            _asp.OnValueChangedSignal.AddListener(UpdateValue);
            UpdateValue(_asp.Current);
        }

        private void Disconnect() => _asp?.OnValueChangedSignal.RemoveListener(UpdateValue);

        private void UpdateValue(float value, float diff = 0)
        {
            _slider.value = value / _asp.Max;
            _counter.text = $"{Mathf.RoundToInt(value)} / {_asp.Max}";
        }

        public void Dispose() => Disconnect();
    }
}