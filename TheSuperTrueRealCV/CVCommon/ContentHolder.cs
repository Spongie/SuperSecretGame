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
        public static SpriteFont Font;

        private static Dictionary<string, Texture2D> ivTextureCache;

        public static void InitOnlyContentManager(ContentManager piManager)
        {
            if(contentManager == null)
                contentManager = piManager;

            ivTextureCache = new Dictionary<string,Texture2D>();
        }

        public static void Init(ContentManager cM)
        {
            InitOnlyContentManager(cM);

            tmp = LoadTexture("tmpAttack");
        }

        public static T LoadExtraContent<T>(string piFileName)
        {
            return contentManager.Load<T>(piFileName);
        }

        public static Texture2D LoadTexture(string piFileName)
        {
            if (ivTextureCache.ContainsKey(piFileName))
                return ivTextureCache[piFileName];

            var asset = contentManager.Load<Texture2D>(piFileName);

            ivTextureCache.Add(piFileName, (Texture2D)asset);

            return asset;
        }

        

    }
}
