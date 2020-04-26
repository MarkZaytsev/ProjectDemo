using Common.FrostLib.Coroutines;
using Common.FrostLib.Services;
using ShipsSection.Scripts.Data;
using ShipsSection.Scripts.Entities;
using ShipsSection.Scripts.Ships;
using UnityEngine;

namespace ShipsSection.Scripts
{
    public class ShipsBootstrapper : MonoBehaviour
    {
        [SerializeField]
        private ShipDataContainer[] _shipsData;
        [SerializeField]
        private ShipsFactory.Context _shipsFactoryContext;

        private static ServiceLocator Servicer => ServiceLocator.Instance;

        private ShipsFactory _factory;
        private MatchProcessor _match;

        private void Start()
        {
            Servicer.Provide((IRoutineRunner)RoutineRunner.Create());
            Servicer.Provide((IEntityFactory)new EntityFactory());
            _factory = new ShipsFactory(_shipsFactoryContext);

            Reload();
        }

        public void Reload()
        {
            Clean();
            Load();
            _match.Start();
        }

        private void Clean() => _match?.Dispose();

        private void Load() => _match = new MatchProcessor(CreateShips());

        private ShipController[] CreateShips()
        {
            var ships = new ShipController[_shipsData.Length];
            for (var i = 0; i < _shipsData.Length; i++)
            {
                var dataContainer = _shipsData[i];
                ships[i] = _factory.Create(dataContainer.Data, dataContainer.Prefab);
            }

            return ships;
        }
    }
}