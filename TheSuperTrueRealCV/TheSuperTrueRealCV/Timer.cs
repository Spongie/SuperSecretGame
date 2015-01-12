using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CV_clone
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
