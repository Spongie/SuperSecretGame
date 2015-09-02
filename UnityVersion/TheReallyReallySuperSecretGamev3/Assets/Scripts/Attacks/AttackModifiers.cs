using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stat;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class AttackModifiers
    {
        public float ApplyAttackEffect(string piEffectName, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var method = GetType().GetMethod(piEffectName, BindingFlags.NonPublic | BindingFlags.Instance);
            
            if(method != null)
            {
                return (float)method.Invoke(this, new object[] { piAttacker, piTarget, piAttackScaling,piEffectPower, piEffectDuration, piEffectTicks, piStats, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float Lifesteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            CStats attackerStats = DamageController.GetGameObjectsStats(piAttacker);

            attackerStats.CurrentHealth += (int)(piCurrentDamage * piEffectPower);

            return piCurrentDamage;
        }

        private float Manasteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            CStats attackerStats = DamageController.GetGameObjectsStats(piAttacker);
            CStats targetStats = DamageController.GetGameObjectsStats(piTarget);

            int amount = (int)(piCurrentDamage * piEffectPower);

            if (amount > targetStats.CurrentMana)
                amount = targetStats.CurrentMana;

            attackerStats.CurrentMana += amount;
            targetStats.CurrentMana -= amount;

            return piCurrentDamage;
        }

        private float ManaDrainDebuff(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new ManaDrainBuff(piEffectPower, piEffectTicks, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float DamageOverTimeDebuff(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new PoisonDebuff(piEffectPower, piEffectTicks, piEffectDuration);
            buff.StatChanges = piStats;

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float MinusAllStats(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new Buff(new CStats((int)piEffectPower), piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float MinusStats(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new Buff(piStats, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Stun(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new StunBuff(piStats, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Freeze(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new ChilledBuff(piStats, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Fear(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piEffectDuration, int piEffectTicks, CStats piStats, float piCurrentDamage)
        {
            var buff = new FearBuff(piStats, piEffectDuration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }
    }
}
