using Assets.Scripts.Attacks.Modifiers;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    [CreateAssetMenu(menuName = "DefenseEffects/Max Damage")]
    public class MaxDamageTaken : Modifier
    {
        public int Max;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            if (piCurrentDamage > Max)
                return Max;

            return piCurrentDamage;
        }
    }
}
