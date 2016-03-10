using Assets.Scripts.Attacks.Modifiers;
using Assets.Scripts.Attacks;
using UnityEngine;
using Assets.Scripts.Buffs;

namespace Assets.Scripts.Defense
{
    [CreateAssetMenu(menuName = "DefenseEffects/Damage as DoT")]
    public class TakeDamageAsDot : Modifier
    {
        public PoisonDebuff Debuff;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(Debuff);

            return 0;
        }
    }
}
