using Assets.Scripts.Attacks.Modifiers;
using Assets.Scripts.Attacks;
using UnityEngine;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Defense
{
    [CreateAssetMenu(menuName = "DefenseEffects/Lucky")]
    public class TakeDamageLucky : Modifier
    {
        public int Chance;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var success = Random.Range(0, 100) < (Chance + piDefender.GetGameObjectsStats().Luck);

            if (!success)
                return piCurrentDamage;

            return 0;
        }
    }
}
