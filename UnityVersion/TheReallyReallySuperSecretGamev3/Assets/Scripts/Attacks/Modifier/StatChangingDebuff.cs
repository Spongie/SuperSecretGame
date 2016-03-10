using Assets.Scripts.Buffs;
using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/Stats Buff")]
    public class StatChangingDebuff : Modifier
    {
        public Buff StatBuff;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(StatBuff);

            return piCurrentDamage;
        }
    }
}
