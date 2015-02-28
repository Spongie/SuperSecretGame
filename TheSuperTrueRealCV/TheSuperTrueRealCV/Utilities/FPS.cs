using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using CVCommon;

namespace CV_clone.Utilities
{
    public class FPS
    {
        public int totalFrames = 0;
        public int fps = 0;
        public float passedTime = 0.0f;

        public void Update(GameTime gameTime)
        {
            passedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
            totalFrames++;
            if (passedTime >= 1000)
            {
                fps = totalFrames;
                passedTime = 0;
                totalFrames = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ContentHolder.Font, "FPS: " + fps.ToString(), new Vector2(0, 400), Color.Red);
        }
    }
}
