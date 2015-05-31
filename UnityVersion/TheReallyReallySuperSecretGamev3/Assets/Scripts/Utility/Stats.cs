using Assets.Scripts.Utility;
using UnityEngine;

namespace CVCommon.Utility
{
	public class Stats : MonoBehaviour
	{
        public CStats stats;

        void Update()
        {
            if (stats.CurrentHealth > stats.MaximumHealth)
                stats.CurrentHealth = stats.MaximumHealth;

            if (stats.CurrentMana > stats.MaximumMana)
                stats.CurrentMana = stats.MaximumMana;
        }
	}
}
