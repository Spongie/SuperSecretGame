using Assets.Scripts.Character;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Character.Monsters;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Utility;
using Assets.Scripts.Defense;

namespace Assets.Scripts.Attacks
{
    public static class DamageController
    {
        private static AttackModifiers attackModifiers = new AttackModifiers();
        private static DefenseModifiers defenseModifiers = new DefenseModifiers();

        public static void DoAttack(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, IEnumerable<AttackEffect> piEffectsFromAttack, Vector3 piHitpoint)
        {
            CStats attackerStats = piAttacker.GetGameObjectsStats();
            CStats targetStats = piDefender.GetGameObjectsStats();

            float baseDamage = (attackerStats.Damage * piAttackScaling.Damage) + (attackerStats.MagicDamage * piAttackScaling.Magic);

            foreach (var effect in piEffectsFromAttack.Concat(GetAttackEffectsFromAttackersEquippedItems(piAttacker)))
            {
                baseDamage = attackModifiers.ApplyAttackEffect(effect, piAttacker, piDefender, piAttackScaling, baseDamage, piHitpoint);
            }

            foreach (var defenseEffect in GetDefenseEffectsFromDefender(piDefender))
            {
                baseDamage = defenseModifiers.ApplyDefenseEffect(defenseEffect, piAttacker, piDefender, piAttackScaling, baseDamage, piHitpoint);
            }

            //Get stats again if they were changed by modifiers
            attackerStats = piAttacker.GetGameObjectsStats();
            targetStats = piDefender.GetGameObjectsStats();

            float physicalDefense = piAttackScaling.Damage > 0 ? targetStats.Defense : 0;

            float magicDefense = piAttackScaling.Magic > 0 ? targetStats.MagicDefense : 0;

            float realDamage = baseDamage - physicalDefense - magicDefense;

            if (piAttackScaling.Damage >= 0 && piAttackScaling.Magic >= 0 && realDamage <= 0)
                realDamage = 1;

            Utility.Logger.Log(string.Format("Dealing {0} damage to {1}", realDamage, piDefender.name));

            DealDamageToGameObject(piDefender, realDamage);
        }

        private static IEnumerable<AttackEffect> GetAttackEffectsFromAttackersEquippedItems(GameObject piAttacker)
        {
            Player player = piAttacker.GetComponent<Player>();

            if (player == null)
                return Enumerable.Empty<AttackEffect>();

            return player.GetAttackEffectsFromEquippedItems();
        }

        private static IEnumerable<DefenseEffect> GetDefenseEffectsFromDefender(GameObject defender)
        {
            Player player = defender.GetComponent<Player>();

            if (player == null)
                return Enumerable.Empty<DefenseEffect>();

            return player.GetDefenseEffectsFromEquippedItems();
        }

        public static void DealDamageToGameObject(GameObject piObject, float piDamage)
        {
            Player player = piObject.GetComponent<Player>();

            if (player != null)
            {
                player.DealDamage(piDamage);
                return;
            }

            piObject.GetComponent<Monster>().CurrentStats.stats.DealDamage(piDamage);
        }

        public static void DrainManaFromGameObject(GameObject piFrom, float piDamage)
        {
            Player player = piFrom.GetComponent<Player>();

            if (player != null)
            {
                player.DrainMana(piDamage);
                return;
            }
            piFrom.GetComponent<Monster>().CurrentStats.stats.DrainMana((int)piDamage);
        }
    }
}
