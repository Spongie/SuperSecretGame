using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace CVCommon
{
    public static class ContentHolder
    {
        public static Texture2D tmp;

        public static ContentManager contentManager;
        public static Texture2D basicGround;
        public static SpriteFont Font;
        
        public static Texture2D player;

        public static Texture2D playerPortrait;
        public static Texture2D barOutline;
        public static Texture2D HPBarFilling;
        public static Texture2D MPBarFilling;

        public static Texture2D castleFloor;
        public static Texture2D castleWall;

        public static Texture2D chatbox;
        public static Texture2D fading;

        public static void InitOnlyContentManager(ContentManager piManager)
        {
            if(contentManager == null)
                contentManager = piManager;

        }

        public static void Init(ContentManager cM)
        {
            InitOnlyContentManager(cM);

            tmp = LoadExtraContent<Texture2D>("tmpAttack");
        }

        public static T LoadExtraContent<T>(string piFileName)
        {
            return contentManager.Load<T>(piFileName);
        }

    }
}
