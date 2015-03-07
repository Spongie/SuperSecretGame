using CVCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSuperTrueRealCV.Interface
{
    public class PlayerPortrait : Entity
    {
        Texture2D outLine;
        Texture2D hpFilling;
        Texture2D xpFilling;
        Texture2D mpFilling;
        Texture2D portrait;

        public PlayerPortrait() :base(ContentHolder.tmp, new Vector2(0,0), new Vector2(300, 300))
        {
            outLine = ContentHolder.LoadTexture("Portrait\\Outline");
            hpFilling = ContentHolder.LoadTexture("Portrait\\RedFilling");
            xpFilling = ContentHolder.LoadTexture("Portrait\\YellowFilling");
            mpFilling = ContentHolder.LoadTexture("Portrait\\BlueFilling");
            portrait = ContentHolder.LoadTexture("Portrait\\Portrait");
            UpdateScreenPosition();
        }

        public void Draw(SpriteBatch spriteBatch, Stats piPlayerStats)
        {
            
            spriteBatch.Draw(outLine, new Rectangle(0, 0, 200, 20), Color.White);
            spriteBatch.Draw(hpFilling, new Rectangle(0, 0, (int)(200 * piPlayerStats.HealthPercentage), 20), new Rectangle(0, 0, hpFilling.Width, hpFilling.Height), Color.White);

            spriteBatch.Draw(outLine, new Rectangle(60, 20, 140, 20), Color.White);
            spriteBatch.Draw(mpFilling, new Rectangle(60, 20, (int)(140 * piPlayerStats.ManaPercentage), 20), Color.White);

            spriteBatch.Draw(outLine, new Rectangle(60, 40, 140, 20), Color.White);
            spriteBatch.Draw(xpFilling, new Rectangle(60, 40, (int)(140 * piPlayerStats.ExpPercentage), 20), Color.White);

            spriteBatch.Draw(portrait, new Rectangle(0, 20, 60, 40), Color.White);
        }
    }
}
