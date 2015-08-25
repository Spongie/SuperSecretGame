using Assets.Scripts.Character.Stat;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DamageDisplayer : MonoBehaviour
    {
        public Transform SpawnPosition;
        public GameObject DamagePrefab;
        public GameObject HealingPrefab;
        private Stats ivStats;

        void Start()
        {
            ivStats = GetComponent<Stats>();
            ivStats.stats.OnDamageTaken += Stats_OnDamageTaken;
        }

        private void Stats_OnDamageTaken(int amount)
        {
            if (amount > 0)
                DisplayDamage(amount);
            else
                DisplayHealing(Mathf.Abs(amount));
        }

        private void DisplayDamage(int amount)
        {
            var textMesh = ((GameObject)Instantiate(DamagePrefab, SpawnPosition.position, Quaternion.identity)).GetComponent<TextMesh>();
            textMesh.text = amount.ToString();
        }

        private void DisplayHealing(int amount)
        {
            var textMesh = ((GameObject)Instantiate(HealingPrefab, SpawnPosition.position, Quaternion.identity)).GetComponent<TextMesh>();
            textMesh.text = amount.ToString();
        }

        void OnDestroy()
        {
            ivStats.stats.OnDamageTaken -= Stats_OnDamageTaken;
        }
    }
}
