using System.Linq;
using UnityEngine;

namespace MetroSection.Data
{
    [CreateAssetMenu(fileName = "Route", menuName = "Metro/Route", order = 0)]
    public class Route : ScriptableObject
    {
        public int Id;
        public Color Color;
        public Station[] Stations;

        public bool ContainsStationById(int id) => Stations.Any(s => s.Id == id);
    }
}