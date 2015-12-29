namespace Assets.Scripts.Utility
{
    public class ManualTimer
    {
        private float timeToRun;
        private float timeRunning;
        private bool ivAutoRestart;
        public event TimerCompleted OnTimerDone;

        public ManualTimer(float secondsToRun, bool autoRestart = false)
        {
            ivAutoRestart = autoRestart;
            timeToRun = secondsToRun;
            Restart();
        }

        public void Restart()
        {
            timeRunning = 0;
        }

        public void Cancel()
        {
            Restart(0);
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

            if (Done && OnTimerDone != null)
                OnTimerDone();

            if (Done && ivAutoRestart)
                Restart();
        }
    }
}
