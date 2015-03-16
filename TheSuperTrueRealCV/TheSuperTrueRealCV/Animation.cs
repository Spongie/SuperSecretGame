using CV_clone.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSuperTrueRealCV
{
    public class Animation
    {
        private Timer ivTimer;
        private int ivIndex;
        private event EventHandler onAnimationFinished;

        public Animation(float piUpdateSpeed):this(piUpdateSpeed ,null, false)
        {

        }

        public Animation(float piUpdateSpeed, EventHandler piOnAnimationDone, bool piLooping)
        {
            if (piOnAnimationDone != null)
                onAnimationFinished += piOnAnimationDone;

            ivTimer = new Timer(piUpdateSpeed);
            IsLooping = piLooping;
        }

        public void Update(GameTime gameTime)
        {
            ivTimer.Update(gameTime);
            if(ivTimer.Done)
            {
                ivIndex++;
                if(ivIndex >= SourceRectangles.Count)
                {
                    if (onAnimationFinished != null)
                        onAnimationFinished(this, new EventArgs());

                    if(IsLooping)
                    {
                        ivTimer.Restart();
                        ivIndex = 0;
                    }
                }
                else
                {
                    ivTimer.Restart();
                }
            }
        }

        public List<Rectangle> SourceRectangles { get; set; }

        public bool IsLooping { get; set; }

        public Rectangle getCurrentRectangle()
        {
            return SourceRectangles[ivIndex];
        }
    }
}
