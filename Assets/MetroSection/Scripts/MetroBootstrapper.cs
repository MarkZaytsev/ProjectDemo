using MetroSection.Data;
using MetroSection.UI;
using UnityEngine;

namespace MetroSection
{
    public class MetroBootstrapper : MonoBehaviour
    {
        [SerializeField]
        private Map _map;
        [SerializeField]
        private MapBuilder.Context _builderContext;

        private MapController _mapController;

        private void Start()
        {
            new ValidateConnectionsCommand(_map.Routes).Execute();
            _mapController = new MapController(_map, new MapBuilder(_builderContext));
        }

        private void OnDestroy() => _mapController.Dispose();
    }
}