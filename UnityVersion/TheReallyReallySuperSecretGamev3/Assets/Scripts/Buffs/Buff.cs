using Assets.Scripts.Character.Stats;
using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts.Buffs
{
    [Serializable]
    public class Buff
    {
        public float Duration;
        public CStats StatChanges;
        protected ManualTimer ivTimer;

        /// <summary>
        /// Creates a StatChanging buff with base duration of 10sec
        /// </summary>
        /// <param name="piStats"></param>
        public Buff(CStats piStats) : this(piStats, 10)
        {
        }

        public Buff(CStats piStats, float piDuration)
        {
            StatChanges = piStats;
            ivTimer = new ManualTimer(piDuration);
            Duration = piDuration;
        }

        public bool Expired
        {
            get { return ivTimer.Done; }
        }

        public virtual void Update(float piDeltaTime)
        {
            if (ivTimer == null)
            {
                ivTimer = new ManualTimer(Duration);
            }

            ivTimer.Update(piDeltaTime);
        }
    }
}
