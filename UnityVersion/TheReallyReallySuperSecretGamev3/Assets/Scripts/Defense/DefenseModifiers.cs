using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using System.Reflection;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    public class DefenseModifiers : Modifiers
    {
        public DefenseModifiers() : base()
        {

        }

        public float ApplyDefenseEffect(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var method = GetType().GetMethod(defenseEffect.Name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                return (float)method.Invoke(this, new object[] { defenseEffect, piAttacker, piDefender, piAttackScaling, piCurrentDamage });
            }

            return piCurrentDamage;
        }

        private float HalfDamage(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            return piCurrentDamage / 2;
        }

        private float DoubleDamage(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            return piCurrentDamage * 2;
        }

        private float DamageAsDot(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var buff = new PoisonDebuff(piCurrentDamage / defenseEffect.Ticks, defenseEffect.Ticks, defenseEffect.Duration);

            var buffContainer = piDefender.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            return 0;
        }

        private float Lucky(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            var success = Random.Range(0, 100) < (defenseEffect.Power + piDefender.GetGameObjectsStats().Luck);

            if (!success)
                return piCurrentDamage;

            return 0;
        }

        private float MaxDamageTaken(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            if (piCurrentDamage > defenseEffect.power)
                return defenseEffect.power;

            return piCurrentDamage;
        }

        private float ArcaneExplosionOnhit(DefenseEffect defenseEffect, GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, float piCurrentDamage, Vector3 piHitpoint)
        {
            GameObject explosion = (GameObject)GameObject.Instantiate(ArcaneExplosion, piHitpoint, Quaternion.identity);
            var attackOfSpell = explosion.GetComponent<Attack>();
            attackOfSpell.Owner = piDefender;
            explosion.SetActive(true);

            return piCurrentDamage;
        }
    }
}
