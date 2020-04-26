using System.Linq;
using UnityEngine;

namespace MetroSection.Data
{
    [CreateAssetMenu(fileName = "Map", menuName = "Metro/Map", order = 2)]
    public class Map : ScriptableObject
    {
        public Route[] Routes;

        public Route[] RoutesByStationId(int id) => Routes.Where(r => r.ContainsStationById(id)).ToArray();
    }
}