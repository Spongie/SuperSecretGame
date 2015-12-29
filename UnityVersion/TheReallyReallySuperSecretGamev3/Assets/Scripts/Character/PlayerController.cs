using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attacks;
using UnityEngine;
using Assets.Scripts.Spells;

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

        public IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems()
        {
            if (!ivInventory.GetEqippedItems().Any())
                return Enumerable.Empty<AttackEffect>();

            return ivInventory.GetEqippedItems().Where(item => item.EffectName != "None").Select(item => new AttackEffect()
                    { Name = item.EffectName, Power = item.EffectValue, Duration = item.EffectDuration, Ticks = item.EffectTicks, Stats = item.EffectStats });
        }

        /// <summary>
        /// Gets base-stats + equipped-stats + Buffstats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return ivBaseStats + Buffs.GetBuffStats();
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

        public Inventory PlayerInventory
        {
            get { return ivInventory; }
        }
    }
}
