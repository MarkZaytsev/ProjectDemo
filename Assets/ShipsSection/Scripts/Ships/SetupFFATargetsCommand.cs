using System.Collections.Generic;
using System.Linq;
using Common;
using Common.FrostLib;
using ShipsSection.Scripts.Aspects;

namespace ShipsSection.Scripts.Ships
{
    // ReSharper disable once InconsistentNaming
    public class SetupFFATargetsCommand : ICommand
    {
        private readonly IReadOnlyCollection<ShipController> _ships;

        public SetupFFATargetsCommand(IReadOnlyCollection<ShipController> shipsController) => 
            _ships = shipsController;

        public void Execute()
        {
            foreach (var ship in _ships)
            {
                var damageAsp = ship.GetAspect<DealDamageToTargetsAspect>();
                var otherShipsTakeDamageAspects =_ships.Where(s => s != ship).Select(s => s.GetAspect<TakeDamageAspect>());
                damageAsp.AddTargets(otherShipsTakeDamageAspects);
            }
        }
    }
}