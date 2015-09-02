using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stat;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Attacks;

namespace Assets.Scripts.Character
{
    [Serializable]
    public class PlayerController : IPlayer
    {
        private IBuffContainer Buffs;
        private CStats ivBaseStats;
        private Inventory ivInventory;

        public PlayerController(IBuffContainer piBuff, CStats piStats)
        {
            ivBaseStats = piStats;
            Buffs = piBuff;
            Buffs.SetStats(ivBaseStats);
            ivInventory = new Inventory();
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
    }
}
