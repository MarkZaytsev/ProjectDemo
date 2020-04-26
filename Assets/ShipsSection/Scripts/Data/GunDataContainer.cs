using UnityEngine;

namespace ShipsSection.Scripts.Data
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Ships/GunData", order = 1)]
    public class GunDataContainer : ScriptableObject
    {
        public GunData Data;
    }
}