using ShipsSection.Scripts.Aspects;
using UnityEngine;

namespace ShipsSection.Scripts.Modules
{
    [CreateAssetMenu(fileName = "HealthFlatBonus", menuName = "Ships/Modules/HealthFlatBonus", order = 0)]
    public class HealthFlatBonusModule : FlatBonusModule<HealthAspect>
    {

    }
}