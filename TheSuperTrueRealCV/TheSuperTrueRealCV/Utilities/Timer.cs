using Microsoft.Xna.Framework;

namespace CV_clone.Utilities
{
    public class Timer
    {
        private float timeToRun;
        private float timeRunning;

        public Timer(float msToRun)
        {
            timeToRun = msToRun;
            timeRunning = 0;
        }

        public bool Done
        {
            get { return timeToRun < timeRunning; }
        }

        public void Update(GameTime time)
        {
            timeRunning += (float)time.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
