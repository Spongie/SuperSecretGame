using UnityEngine;

namespace Assets.Scripts.Character.Stat
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

        public bool IsDead()
        {
            return stats.CurrentHealth <= 0;
        }
	}
}
