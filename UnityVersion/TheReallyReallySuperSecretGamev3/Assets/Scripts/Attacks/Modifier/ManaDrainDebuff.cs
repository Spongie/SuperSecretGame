using Assets.Scripts.Buffs;
using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/Manadrain DoT")]
    public class ManaDrainDebuff : Modifier
    {
        [Tooltip("The debuff to apply")]
        public ManaDrainBuff ManaDrainEffect;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(ManaDrainEffect);

            return piCurrentDamage;
        }
    }
}
