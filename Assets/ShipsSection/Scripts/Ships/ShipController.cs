using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Ships
{
    [RequireComponent(typeof(ShipView))]
    public class ShipController : MonoBehaviour
    {
        public string Identity => GetAspect<IdentityAspect>().ToString();

        private ShipView _view;

        private AspectedEntity _entity;

        private void Awake() => _view = GetComponent<ShipView>();

        public void Initialize(AspectedEntity entity)
        {
            _entity = entity;

            GetAspect<DeathAspect>().OnTriggerSignal.AddOnce(HandleDeath);

            name = GetAspect<IdentityAspect>().ToString();

            ConnectView();
        }

        private void ConnectView()
        {
            _view.ConnectToHealth(GetAspect<HealthAspect>());
            _view.ConnectToShield(GetAspect<ShieldAspect>());

            foreach (var info in GetAspect<GunsAspect>().GetGunsInfo())
                _view.ConnectToGun(info.Name, info.OnReloadSignal);
        }

        private void HandleDeath()
        {
            Debug.Log($"{name} killed");
            DestroySelf();
        }

        public T GetAspect<T>() where T : class => _entity.GetAspect<T>();

        private void OnDestroy() => _entity.Dispose();

        public void DestroySelf()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        public void Fire() => GetAspect<GunsAspect>().Fire();

        public void HoldFire() => GetAspect<GunsAspect>().Hold();
    }
}