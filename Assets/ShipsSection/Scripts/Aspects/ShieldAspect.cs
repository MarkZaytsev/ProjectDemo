using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public class ShieldAspect : ResourceAspect, IDamageHandler
    {
        public ShieldAspect(int defaultValue) : base(defaultValue) { }

        public float Handle(float damage)
        {
            if (Mathf.Approximately(Container.Value, 0))
                return damage;

            var shieldLeft = Container.Value - damage;
            Set(Mathf.Max(shieldLeft, 0));

            return shieldLeft >= 0f ? 0f : Mathf.Abs(shieldLeft);
        }
    }
}