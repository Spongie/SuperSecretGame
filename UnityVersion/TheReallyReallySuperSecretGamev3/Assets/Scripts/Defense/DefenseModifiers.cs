﻿using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    public class DefenseModifiers
    {
        public float ApplyDefenseEffect(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage)
        {
            var method = GetType().GetMethod(defenseEffect.Name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                return (float)method.Invoke(this, new object[] { defenseEffect, piAttacker, piDefender, piAttackScaling, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float HalfDamage(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage)
        {
            return piCurrentDamage / 2;
        }

        private float DoubleDamage(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage)
        {
            return piCurrentDamage * 2;
        }

        private float DamageAsDot(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage)
        {
            var buff = new PoisonDebuff(piCurrentDamage / defenseEffect.Ticks, defenseEffect.Ticks, defenseEffect.Duration);

            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return 0;
        }

        private float Lucky(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage)
        {
            var stats = DamageController.GetGameObjectsStats(piDefender);

            var success = Random.Range(0, 100) < (defenseEffect.Power + stats.Luck);

            if (!success)
                return piCurrentDamage;

            return 0;
        }
    }
}
