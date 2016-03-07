using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attacks;
using UnityEngine;
using Assets.Scripts.Spells;
using Assets.Scripts.Defense;
using Assets.Scripts.Attacks.Modifier;

namespace Assets.Scripts.Character
{
    [Serializable]
    public class PlayerController : IPlayer
    {
        private IBuffContainer Buffs;
        private CStats ivBaseStats;
        private Inventory ivInventory;
        public SpellManager SpellController;

        public PlayerController(IBuffContainer piBuff, CStats piStats, SpellManager piSpellmanager)
        {
            ivBaseStats = piStats;
            Buffs = piBuff;
            Buffs.SetStats(ivBaseStats);
            ivInventory = new Inventory();
            SpellController = piSpellmanager;
        }

        public IEnumerable<AttackModifier> GetAttackEffectsFromEquippedItems()
        {
            if (!ivInventory.GetEqippedItems().Any())
                return Enumerable.Empty<AttackModifier>();

            var attackEffects = new List<AttackModifier>();

            foreach (var item in ivInventory.GetEqippedItems().Where(item => item.AttackEffects.Any()))
            {
                foreach (var attackEffect in item.AttackEffects)
                {
                    attackEffects.Add(attackEffect);
                }
            }

            return attackEffects;
        }

        /// <summary>
        /// Gets base-stats + equipped-stats + Buffstats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return ivBaseStats + Buffs.GetBuffStats() + ivInventory.GetEquippedStats();
        }

        public void GiveLoot(Item piDrop)
        {
            ivInventory.AddItem(piDrop);
        }

        public bool LoadPlayer()
        {
            return true;
        }

        public void RewardExp(int piAmount)
        {
            ivBaseStats.RewardExperience(piAmount);
        }

        public IBuffContainer GetBuffContainer()
        {
            return Buffs;
        }

        public void DealDamage(float amount)
        {
            ivBaseStats.DealDamage(amount);
        }

        public void DrainMana(float amount)
        {
            ivBaseStats.DrainMana((int)amount);
        }

        public List<GameObject> GetAvailableSpells()
        {
            return SpellController.GetAvailableSpells();
        }

        public bool CanCastSpell(SpellSlot piSpellSlot)
        {
            return SpellController.CanCastSpell(piSpellSlot, GetTrueStats().Resources.CurrentMana);
        }

        public IEnumerable<DefenseEffect> GetDefenseEffectsFromEquippedItems()
        {
            if (!ivInventory.GetEqippedItems().Any())
                return Enumerable.Empty<DefenseEffect>();

            var defenseEffects = new List<DefenseEffect>();

            foreach (var item in ivInventory.GetEqippedItems().Where(item => item.AttackEffects.Any()))
            {
                foreach (var defenseEffect in item.DefenseEffects)
                {
                    defenseEffects.Add(defenseEffect);
                }
            }

            return defenseEffects;
        }

        public Inventory PlayerInventory
        {
            get { return ivInventory; }
        }
    }
}
