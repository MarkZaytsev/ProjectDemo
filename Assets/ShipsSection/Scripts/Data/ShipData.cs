using System;
using ShipsSection.Scripts.Modules;

namespace ShipsSection.Scripts.Data
{
    [Serializable]
    public struct ShipData
    {
        public string Name;
        public int Health;
        public ShieldData Shield;

        public GunDataContainer[] Guns;
        public Module[] Modules;
    }
}