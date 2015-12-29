using Assets.Scripts.Character.Stats;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DamageDisplayer : MonoBehaviour
    {
        public Transform SpawnPosition;
        public GameObject DamagePrefab;
        public GameObject HealingPrefab;
        private EntityStats ivStats;

        void Start()
        {
            ivStats = GetComponent<EntityStats>();
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

            if (amount < 15)
                textMesh.fontSize = (int)(textMesh.fontSize * 0.75f);
        }

        private void DisplayHealing(int amount)
        {
            var textMesh = ((GameObject)Instantiate(HealingPrefab, SpawnPosition.position, Quaternion.identity)).GetComponent<TextMesh>();
            textMesh.text = amount.ToString();

            if (amount < 15)
                textMesh.fontSize = (int)(textMesh.fontSize * 0.75f);
        }

        void OnDestroy()
        {
            ivStats.stats.OnDamageTaken -= Stats_OnDamageTaken;
        }
    }
}
