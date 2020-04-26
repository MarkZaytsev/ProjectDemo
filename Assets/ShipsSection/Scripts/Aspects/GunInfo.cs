using Common.FrostLib.Signals.impl;

namespace ShipsSection.Scripts.Aspects
{
    public struct GunInfo
    {
        public readonly string Name;
        public readonly Signal<float> OnReloadSignal;

        public GunInfo(string name, Signal<float> onReloadSignal)
        {
            Name = name;
            OnReloadSignal = onReloadSignal;
        }
    }
}