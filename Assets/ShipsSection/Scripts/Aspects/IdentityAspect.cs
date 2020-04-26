using Common.FrostLib.Aspects;

namespace ShipsSection.Scripts.Aspects
{
    public class IdentityAspect : Aspect
    {
        private readonly string _name;

        public IdentityAspect(string name) => _name = name;

        public override string ToString() => _name;
    }
}