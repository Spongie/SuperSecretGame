using Assets.Scripts.Buffs;
using Assets.Scripts.Utility;
using System.Reflection;
using TheSuperTrueRealCV.Utilities;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class AttackModifiers
    {
        public float ApplyAttackEffect(string piEffectName, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            var method = GetType().GetMethod(piEffectName, BindingFlags.NonPublic | BindingFlags.Instance);
            
            if(method != null)
            {
                return (float)method.Invoke(this, new object[] { piAttacker, piTarget, piAttackScaling,piEffectPower, piEffectDuration, piEffectTicks, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float Lifesteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            CStats attackerStats = DamageCalculator.GetGameObjectsStats(piAttacker);

            attackerStats.CurrentHealth += (int)(piCurrentDamage * piEffectPower);

            return piCurrentDamage;
        }

        private float Manasteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            CStats attackerStats = DamageCalculator.GetGameObjectsStats(piAttacker);
            CStats targetStats = DamageCalculator.GetGameObjectsStats(piTarget);

            int amount = (int)(piCurrentDamage * piEffectPower);

            if (amount > targetStats.CurrentMana)
                amount = targetStats.CurrentMana;

            attackerStats.CurrentMana += amount;
            targetStats.CurrentMana -= amount;

            return piCurrentDamage;
        }

        private float ManaDrainDebuff(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            var buff = new ManaDrainBuff(piEffectPower, piEffectTicks, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float DamageOverTimeDebuff(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            var buff = new PoisonDebuff(piEffectPower, piEffectTicks, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float MinusAllStats(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, float piCurrentDamage)
        {
            var buff = new Buff(new CStats((int)piEffectPower));

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }
    }
}
