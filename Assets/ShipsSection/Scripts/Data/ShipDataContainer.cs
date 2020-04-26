using UnityEngine;

namespace ShipsSection.Scripts.Data
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "Ships/ShipData", order = 0)]
    public class ShipDataContainer : ScriptableObject
    {
        public GameObject Prefab;
        public ShipData Data;
    }
}