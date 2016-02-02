using Assets.Scripts.Attacks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Defense
{
    [Serializable]
    public class DefenseEffect : AttackEffect
    {
        [NonSerialized]
        private DefenseEffectLoader ivDefenseEffectLoader;

        public DefenseEffect() : base()
        {
            ivDefenseEffectLoader = new DefenseEffectLoader();
        }

        public List<string> DefenseModifiers
        {
            get
            {
                if (ivDefenseEffectLoader == null)
                    ivDefenseEffectLoader = new DefenseEffectLoader();

                return ivDefenseEffectLoader.GetDefenseMethods();
            }
        }

    }
}
