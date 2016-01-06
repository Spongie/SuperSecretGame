using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EquippedStatsDisplayer : MonoBehaviour
    {
        public Text HpText;
        public Text MpText;
        public Text HpRegenText;
        public Text MpRegenText;
        public Text DamageText;
        public Text DefenseText;
        public Text MDamageText;
        public Text MDefenseText;
        public Text LuckText;
        public Text ResistanceText;
        public Player player;

        void Update()
        {
            var stats = player.Controller.GetTrueStats();

            HpText.text = stats.Resources.MaximumHealth.ToString();
            MpText.text = stats.Resources.MaximumMana.ToString();
            HpRegenText.text = stats.HealthPerSecond.ToString("F1");
            MpRegenText.text = stats.ManaPerSecond.ToString("F1");

            DamageText.text = stats.Damage.ToString();
            DefenseText.text = stats.Defense.ToString();
            MDamageText.text = stats.MagicDamage.ToString();
            MDefenseText.text = stats.MagicDefense.ToString();
            LuckText.text = stats.Luck.ToString();
            ResistanceText.text = stats.Resistance.ToString();
        }
    }
}
