using System.Linq;
using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Aspects;
using ShipsSection.Scripts.Data;

namespace ShipsSection.Scripts.Entities
{
    public class EntityFactory : IEntityFactory
    {
        //ignore dispose warning. aspects are disposed by entity
        public AspectedEntity Create(ShipData data)
        {
            var entity = new AspectedEntity(
                new LogDealDamageAspect(),
                new LogResourceAspect<HealthAspect>("Health"),
                new LogResourceAspect<ShieldAspect>("Shield"),
                new IdentityAspect(data.Name),
                new HealthAspect(data.Health),
                new TakeDamageAspect(),
                new ShieldAspect(data.Shield.Value),
                new RechargeShieldAspect(data.Shield.RechargeRate),
                new DeathAspect(),
                new DealDamageToTargetsAspect(),
                new GunsAspect(data.Guns.Select(c => c.Data).ToArray()));

            SetupDamageHandlers(entity);

            foreach (var module in data.Modules)
                module.Apply(entity);

            return entity;
        }

        private static void SetupDamageHandlers(AspectedEntity entity)
        {
            var takeDamageAspect = entity.GetAspect<TakeDamageAspect>();
            takeDamageAspect.AddHandler(entity.GetAspect<ShieldAspect>());
        }
    }
}