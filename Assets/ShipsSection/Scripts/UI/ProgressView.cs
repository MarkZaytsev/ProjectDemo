using System.Collections;
using Common.FrostLib.Signals.impl;
using UnityEngine;
using UnityEngine.UI;

namespace ShipsSection.Scripts.UI
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Text _title;

        private Signal<float> _signal;

        public void Initialize(string title, Signal<float> signal)
        {
            Disconnect();

            _title.text = title;

            _signal = signal;
            _signal.AddListener(Restart);
        }

        private void Disconnect() => _signal?.RemoveListener(Restart);

        private void Restart(float duration) => StartCoroutine(Progress(duration));

        public IEnumerator Progress(float duration)
        {
            var wait = new WaitForEndOfFrame();
            var endTime = Time.timeSinceLevelLoad + duration;
            while (Time.timeSinceLevelLoad <= endTime)
            {
                var diff = endTime - Time.timeSinceLevelLoad;
                _slider.value = (duration - diff) / duration;
                yield return wait;
            }

            _slider.value = 1f;
        }

        private void OnDestroy() => Disconnect();
    }
}