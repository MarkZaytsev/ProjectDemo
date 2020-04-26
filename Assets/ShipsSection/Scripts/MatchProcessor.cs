using System.Collections.Generic;
using ShipsSection.Scripts.Aspects;
using ShipsSection.Scripts.Ships;
using UnityEngine;

namespace ShipsSection.Scripts
{
    public class MatchProcessor
    {
        private readonly List<ShipController> _ships = new List<ShipController>();

        public MatchProcessor(IEnumerable<ShipController> ships) => _ships.AddRange(ships);

        public void Start()
        {
            new SetupFFATargetsCommand(_ships).Execute();
            SubToShipKilled();
            AllFire();
        }

        private void SubToShipKilled()
        {
            foreach (var ship in _ships)
                ship.GetAspect<DeathAspect>().OnTriggerSignal.AddListener(() => OnShipKilled(ship));
        }

        private void OnShipKilled(ShipController ship)
        {
            _ships.Remove(ship);
            
            foreach (var otherShip in _ships)
                otherShip.GetAspect<DealDamageToTargetsAspect>().RemoveTarget(ship.GetAspect<TakeDamageAspect>());

            CheckEnd();
        }

        private void AllFire()
        {
            foreach (var ship in _ships)
                ship.Fire();
        }

        private void CheckEnd()
        {
            switch (_ships.Count)
            {
                case 0:
                    Debug.Log("Draw");
                    return;
                case 1:
                    var ship = _ships[0];
                    ship.HoldFire();
                    Debug.Log($"{ship.Identity} is the winner!");
                    break;
            }
        }

        public void Dispose()
        {
            foreach (var ship in _ships)
                ship.DestroySelf();
        }
    }
}