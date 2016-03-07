﻿using Assets.Scripts.Buffs;
using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/DoT")]
    public class DamageOverTimeDebuff : AttackModifier
    {
        public PoisonDebuff Debuff;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(Debuff);

            return piCurrentDamage;
        }
    }
}
