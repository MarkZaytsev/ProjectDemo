using System.Linq;
using UnityEngine;

namespace MetroSection.Data
{
    [CreateAssetMenu(fileName = "Station", menuName = "Metro/Station", order = 1)]
    public class Station : ScriptableObject
    {
        public int Id;
        public string Name;
        public Vector2 Position;
        public Station[] Connections;

        public bool IsConnected(Station s) => Connections.Contains(s);
    }
}