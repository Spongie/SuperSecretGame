using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    public class ManualTimer
    {
        private float timeToRun;
        private float timeRunning;

        public ManualTimer(float secondsToRun)
        {
            timeToRun = secondsToRun;
            Restart();
        }

        public void Restart()
        {
            timeRunning = 0;
        }

        public bool Done
        {
            get { return timeToRun < timeRunning; }
        }

        public void Restart(float piTimeToRun)
        {
            timeToRun = piTimeToRun;
            timeRunning = 0;
        }

        public void Update(float piDeltaTime)
        {
            timeRunning += piDeltaTime;
        }
    }
}
