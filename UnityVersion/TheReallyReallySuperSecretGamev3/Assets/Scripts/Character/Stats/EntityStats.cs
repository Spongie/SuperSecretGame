using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Character.Stats
{
    public class EntityStats : MonoBehaviour
	{
        public CStats stats;
        public ManualTimer ivRegenTimer;

        void Start()
        {
            ivRegenTimer = new ManualTimer(1f, true);
            ivRegenTimer.OnTimerDone += IvRegenTimer_OnTimerDone;
        }

        private void IvRegenTimer_OnTimerDone()
        {
            stats.RegenTick();
        }

        void Update()
        {
            ivRegenTimer.Update(Time.deltaTime);
        }

        public bool IsDead()
        {
            return stats.Resources.IsDead();
        }
	}
}
