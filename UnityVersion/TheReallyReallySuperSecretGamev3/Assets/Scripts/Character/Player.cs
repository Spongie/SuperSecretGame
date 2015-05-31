using Assets.Scripts.ResourceManagers;
using Assets.Scripts.Utility;
using CVCommon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(Stats))]
    public class Player : MonoBehaviour
    {
        private CStats ivBaseStats;
        private Inventory ivInventory;

        void Start()
        {
            ivBaseStats = GetComponent<Stats>().stats;
            ivInventory = new Inventory();

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
        /// Gets base-stats + equipped-stats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return ivBaseStats;
        }

        private bool LoadPlayer()
        {
            return false;
        }
    }
}
