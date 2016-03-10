using Assets.Scripts.Attacks.Modifiers;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    [CreateAssetMenu(menuName = "DefenseEffects/Multiplied Damage")]
    public class TakeMultipliedDamage : Modifier
    {
        public float Multiplier;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            return piCurrentDamage * Multiplier;
        }
    }
}
