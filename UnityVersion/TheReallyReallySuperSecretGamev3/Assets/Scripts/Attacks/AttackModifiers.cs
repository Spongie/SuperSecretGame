using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using System.Reflection;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class AttackModifiers : Modifiers
    {
        public AttackModifiers() : base()
        {

        }

        public float ApplyAttackEffect(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var method = GetType().GetMethod(attackEffect.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            
            if(method != null)
            {
                return (float)method.Invoke(this, new object[] { attackEffect, piAttacker, piTarget, piAttackScaling, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float Lifesteal(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            DamageController.DealDamageToGameObject(piAttacker, -(int)(piCurrentDamage * attackEffect.Power));

            return piCurrentDamage;
        }

        private float Manasteal(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            CStats targetStats = piTarget.GetGameObjectsStats();
                
            int amount = (int)(piCurrentDamage * attackEffect.Power);

            if (amount > targetStats.Resources.CurrentMana)
                amount = targetStats.Resources.CurrentMana;

            DamageController.DrainManaFromGameObject(piTarget, amount);
            DamageController.DrainManaFromGameObject(piAttacker, -amount);

            return piCurrentDamage;
        }

        private float ManaDrainDebuff(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new ManaDrainBuff(attackEffect.Power, attackEffect.Ticks, attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float DamageOverTimeDebuff(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new PoisonDebuff(attackEffect.Power, attackEffect.Ticks, attackEffect.Duration);
            buff.StatChanges = attackEffect.Stats;

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }


        private float MinusAllStats(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new Buff(new CStats((int)attackEffect.Power), attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float MinusStats(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new Buff(attackEffect.Stats, attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Stun(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new StunBuff(attackEffect.Stats, attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Freeze(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new ChilledBuff(attackEffect.Stats, attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float Fear(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new FearBuff(attackEffect.Stats, attackEffect.Duration);

            var buffContainer = piTarget.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return piCurrentDamage;
        }

        private float OneShot(AttackEffect attackEffect, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            if (Random.Range(0, 100f) < attackEffect.Power)
                return float.MaxValue;

            return piCurrentDamage;
        }
    }
}
