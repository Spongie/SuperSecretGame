using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CVCommon.Interface
{
    class Button
    {
        Texture2D tex;
        Point position;
        Point size;

        public event EventHandler OnClick;

        public Button(Texture2D te, Point pos, Point sz)
        {
            tex = te;
            position = pos;
            size = sz;
        }

        public Rectangle Rect
        {
            get { return new Rectangle(position.X, position.Y, size.X, size.Y); }
        }

        public virtual void Update()
        {
            if (Checklick(KeyMouseReader.mouseState, KeyMouseReader.oldMouseState))
                if (OnClick != null)
                    OnClick(this, null);
        }

        public virtual bool Checklick(MouseState mouseState, MouseState oldState)
        {
            if (Rect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, Rect, Color.White);
        }
    }
}
