using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheSuperTrueRealCV.Utilities;

namespace Assets.Scripts.Attacks
{
    public class AttackModifiers
    {
        public float ApplyAttackEffect(string piEffectName, CStats piAttacker, CStats piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
        {
            var method = GetType().GetMethod(piEffectName, BindingFlags.NonPublic | BindingFlags.Instance);
            
            if(method != null)
            {
                return (float)method.Invoke(this, new object[] { piAttacker, piTarget, piAttackScaling,piEffectPower, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float Lifesteal(CStats piAttacker, CStats piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
        {
            piAttacker.CurrentHealth += (int)(piCurrentDamage * piEffectPower);

            return piCurrentDamage;
        }

        private float Manasteal(CStats piAttacker, CStats piTarget, AttackDamageScaling piAttackScaling, float piEffectPower, float piCurrentDamage)
        {
            int amount = (int)(piCurrentDamage * piEffectPower);

            if (amount > piTarget.CurrentMana)
                amount = piTarget.CurrentMana;

            piAttacker.CurrentMana += amount;
            piTarget.CurrentMana -= amount;

            return piCurrentDamage;
        }
    }
}
