﻿using Assets.Scripts.Buffs;
using UnityEngine;

namespace Assets.Scripts.Attacks.Modifiers
{
    [CreateAssetMenu(menuName = "AttackEffects/Fear")]
    public class Fear : Modifier
    {
        public FearBuff FearModifier;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(FearModifier);

            return piCurrentDamage;
        }
    }
}
