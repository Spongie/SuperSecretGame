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

        void Start()
        {
            ivBaseStats = GetComponent<Stats>().stats;

            if(!LoadPlayer())
                SetBaseStats();
        }

        private void SetBaseStats()
        {
            ivBaseStats.MaximumHealth = 100;
            ivBaseStats.CurrentHealth = 100;
            ivBaseStats.MaximumMana = 50;
            ivBaseStats.CurrentMana = 50;
        }

        /// <summary>
        /// Gets base-stats + equipped-stats
        /// </summary>
        /// <returns></returns>
        private CStats GetTrueStats()
        {
            return null;
        }

        private bool LoadPlayer()
        {
            return false;
        }
    }
}
