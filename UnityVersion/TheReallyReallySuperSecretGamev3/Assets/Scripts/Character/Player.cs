using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using Assets.Scripts.ResourceManagers;
using Assets.Scripts.Utility;
using CVCommon.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(BuffContainer))]
    public class Player : MonoBehaviour
    {
        private BuffContainer Buffs;
        private CStats ivBaseStats;
        private Inventory ivInventory;

        void Start()
        {
            ivBaseStats = GetComponent<Stats>().stats;
            ivInventory = new Inventory();
            Buffs = GetComponent<BuffContainer>();
            Buffs.Stats = ivBaseStats;

            if(!LoadPlayer())
                SetBaseStats();
        }

        private void SetBaseStats()
        {
            ivBaseStats.MaximumHealth = 100;
            ivBaseStats.CurrentHealth = 100;
            ivBaseStats.MaximumMana = 50;
            ivBaseStats.CurrentMana = 50;
            ivBaseStats.MaximumExp = 100;
            ivBaseStats.Level = 1;
        }

        /// <summary>
        /// Gets base-stats + equipped-stats + Buffstats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return ivBaseStats + Buffs.GetBuffStats();
        }

        public IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems()
        {
            return ivInventory.GetEqippedItems().Where(item => item.EffectName != "None").Select(item => new AttackEffect() { Name = item.EffectName, Power = item.EffectValue, Duration = item.EffectDuration, Ticks = item.EffectTicks });
        }

        private bool LoadPlayer()
        {
            return false;
        }
    }
}
