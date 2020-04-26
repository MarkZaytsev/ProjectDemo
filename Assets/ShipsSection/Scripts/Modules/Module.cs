using Common.FrostLib.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Modules
{
    public abstract class Module : ScriptableObject
    {
        public abstract void Apply(AspectedEntity entity);
    }
}