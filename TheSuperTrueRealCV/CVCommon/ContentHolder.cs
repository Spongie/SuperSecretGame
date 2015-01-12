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
        public static ContentManager contentManager;
        public static Texture2D basicGround;
        public static SpriteFont Font;
        //Player
        public static Texture2D player;

        //Portrait -> HP/MP Bar
        public static Texture2D playerPortrait;
        public static Texture2D barOutline;
        public static Texture2D HPBarFilling;
        public static Texture2D MPBarFilling;

        //Grounds
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
            basicGround = contentManager.Load<Texture2D>("testGround");
            player = contentManager.Load<Texture2D>("Player/SuperSheet");
            playerPortrait = contentManager.Load<Texture2D>("Portrait");
            barOutline = contentManager.Load<Texture2D>("BarOutline");
            HPBarFilling = contentManager.Load<Texture2D>("HPFilling");
            MPBarFilling = contentManager.Load<Texture2D>("MPBar");
            castleFloor = contentManager.Load<Texture2D>("CasteFloor");
            castleWall = contentManager.Load<Texture2D>("CastleWall");
            Font = contentManager.Load<SpriteFont>("Font");
            chatbox = contentManager.Load<Texture2D>("Chatbox");
            fading = contentManager.Load<Texture2D>("empty");
        }

        public static T LoadExtraContent<T>(string piFileName)
        {
            return contentManager.Load<T>(piFileName);
        }
    }
}
