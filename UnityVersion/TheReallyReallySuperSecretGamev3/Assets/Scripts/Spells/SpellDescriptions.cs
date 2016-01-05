using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Spells
{
    public class SpellDescriptions
    {
        public static string GetSpellDescription(string spellName)
        {
            switch (spellName)
            {
                case "Fireball":
                    return "Shoots a ball of fire dealing 100% of your magic power in damage";
                case "Heal":
                    return "Instantly heals you for 100% of your magic power";
                default:
                    return string.Empty;
            }
        }
    }
}
