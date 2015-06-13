using Assets.Scripts.Utility;
using CVCommon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheSuperTrueRealCV.Utilities;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class AttackModifiers
    {
        public float ApplyAttackEffect(string piEffectName, GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
        {
            var method = GetType().GetMethod(piEffectName, BindingFlags.NonPublic | BindingFlags.Instance);
            
            if(method != null)
            {
                return (float)method.Invoke(this, new object[] { piAttacker, piTarget, piAttackScaling,piEffectPower, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float Lifesteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
        {
            CStats attackerStats = DamageCalculator.GetGameObjectsStats(piAttacker);

            attackerStats.CurrentHealth += (int)(piCurrentDamage * piEffectPower);

            return piCurrentDamage;
        }

        private float Manasteal(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
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
    }
}
