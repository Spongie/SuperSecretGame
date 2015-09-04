using Assets.Scripts.Character;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Character.Monsters;
using Assets.Scripts.Character.Stat;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Attacks
{
    public static class DamageController
    {
        private static AttackModifiers attackModifiers = new AttackModifiers();

        public static void DoAttack(GameObject piAttacker, GameObject piTarget, AttackDamageScaling piAttackScaling, IEnumerable<AttackEffect> piEffectsFromAttack)
        {
            CStats attackerStats = GetGameObjectsStats(piAttacker);
            CStats targetStats = GetGameObjectsStats(piTarget);

            float baseDamage = (attackerStats.Damage * piAttackScaling.Damage) + (attackerStats.MagicDamage * piAttackScaling.Magic);

            float physicalDefense = piAttackScaling.Damage > 0 ? targetStats.Defense : 0;

            float magicDefense = piAttackScaling.Magic > 0 ? targetStats.MagicDefense : 0;

            foreach (var effect in piEffectsFromAttack.Concat(GetAttackEffectsFromAttackersEquippedItems(piAttacker)))
            {
                baseDamage = attackModifiers.ApplyAttackEffect(effect.Name, piAttacker, piTarget, piAttackScaling, effect.Power, effect.Duration, effect.Ticks, effect.Stats, baseDamage);
            }

            float realDamage = baseDamage - physicalDefense - magicDefense;

            Logger.Log(string.Format("Dealing {0} damage to {1}", realDamage, piTarget.name));

            DealDamageToGameObject(piTarget, realDamage);
        }

        private static IEnumerable<AttackEffect> GetAttackEffectsFromAttackersEquippedItems(GameObject piAttacker)
        {
            Player player = piAttacker.GetComponent<Player>();

            if (player == null)
                return Enumerable.Empty<AttackEffect>();

            return player.GetAttackEffectsFromEquippedItems();
        }

        public static void DealDamageToGameObject(GameObject piObject, float piDamage)
        {
            Player player = piObject.GetComponent<Player>();

            if (player != null)
                player.DealDamage(piDamage);

            piObject.GetComponent<Monster>().CurrentStats.stats.DealDamage(piDamage);
        }

        public static void DrainManaFromGameObject(GameObject piFrom, float piDamage)
        {
            Player player = piFrom.GetComponent<Player>();

            if (player != null)
                player.DrainMana(piDamage);

            piFrom.GetComponent<Monster>().CurrentStats.stats.DrainMana((int)piDamage);
        }

        public static CStats GetGameObjectsStats(GameObject piObject)
        {
            Player player = piObject.GetComponent<Player>();

            if (player != null)
                return player.GetTrueStats();

            return piObject.GetComponent<Monster>().GetTrueStats();
        }
    }
}
