using Common.FrostLib.Signals.impl;
using ShipsSection.Scripts.Aspects;
using ShipsSection.Scripts.UI;
using UnityEngine;

namespace ShipsSection.Scripts.Ships
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField]
        private ResourceView _health;
        [SerializeField]
        private ResourceView _shield;
        [SerializeField]
        private ProgressView _gunReloadPrefab;
        [SerializeField]
        private Transform _gunsReloadContainer;

        public void ConnectToHealth(ResourceAspect asp) => _health.Connect(asp);

        public void ConnectToShield(ResourceAspect asp) => _shield.Connect(asp);

        public void ConnectToGun(string title, Signal<float> reloadSignal)
        {
            var go = Instantiate(_gunReloadPrefab.gameObject, _gunsReloadContainer);
            var view = go.GetComponent<ProgressView>();
            view.Initialize(title, reloadSignal);
        }

        private void OnDestroy()
        {
            _health.Dispose();
            _shield.Dispose();
        }
    }
}