using Common.FrostLib.Aspects;
using ShipsSection.Scripts.Data;

namespace ShipsSection.Scripts.Entities
{
    public interface IEntityFactory
    {
        AspectedEntity Create(ShipData data);
    }
}