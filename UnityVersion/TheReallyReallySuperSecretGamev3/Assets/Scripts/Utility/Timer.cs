using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public delegate void TimerCompleted();

    public class Timer : MonoBehaviour
    {
        public event TimerCompleted OnTimerDone;

        public float TimeToRun;
        public float TimeRunning;

        public void Restart()
        {
            TimeRunning = 0;
        }

        public bool Done
        {
            get { return TimeToRun < TimeRunning; }
        }

        public void Restart(float piTimeToRun)
        {
            TimeToRun = piTimeToRun;
            TimeRunning = 0;
        }

        void Update()
        {
            TimeRunning += Time.deltaTime;

            if (Done && OnTimerDone != null)
                OnTimerDone();
        }
    }
}
