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

namespace CV_clone
{
    public class Item : Entity
    {
        ItemType type;

        public Item(ItemType type, Texture2D tex)
            : base(tex, Vector2.Zero, Vector2.Zero)
        {
            this.type = type;
        }

        public virtual void DrawIcon(SpriteBatch spriteBatch, Rectangle r)
        {
            spriteBatch.Draw(texture, r, Color.White);
        }
    }
}
