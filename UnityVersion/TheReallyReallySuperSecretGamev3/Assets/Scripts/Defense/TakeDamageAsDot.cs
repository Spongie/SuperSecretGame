using Assets.Scripts.Attacks.Modifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Attacks;
using UnityEngine;
using Assets.Scripts.Buffs;

namespace Assets.Scripts.Defense
{
    public class TakeDamageAsDot : Modifier
    {
        public PoisonDebuff Debuff;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(Debuff);

            return 0;
        }
    }
}
