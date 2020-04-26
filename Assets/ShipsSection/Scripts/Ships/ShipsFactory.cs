using Common.FrostLib.Services;
using ShipsSection.Scripts.Data;
using ShipsSection.Scripts.Entities;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShipsSection.Scripts.Ships
{
    public class ShipsFactory
    {
        [Serializable]
        public struct Context
        {
            public Transform Parent;
        }

        private static ServiceLocator Servicer => ServiceLocator.Instance;
        private static IEntityFactory Entitory => Servicer.Get<IEntityFactory>();

        private readonly Context _context;

        public ShipsFactory(Context context) => _context = context;

        public ShipController Create(ShipData data, GameObject prefab)
        {
            var ship = Object.Instantiate(prefab.gameObject, _context.Parent).GetComponent<ShipController>();
            ship.Initialize(Entitory.Create(data));

            return ship;
        }
    }
}