using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CVCommon
{
    public class Entity
    {
        protected Texture2D texture;
        protected Direction direction;

        public Entity(Texture2D tex, Vector2 pos, Vector2 size)
        {
            texture = tex;
            this.Size = size;
            WorldPosition = pos;
            ScreenPosition = Vector2.Zero;
            direction = Direction.Right;
        }

        public Entity()
        {
            // TODO: Complete member initialization
        }

        public Rectangle Rect
        {
            get { return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y); }
        }

        public Rectangle ScreenRect
        {
            get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y); }
        }

        public Vector2 WorldPosition { get; set; }

        public Vector2 Size { get; set; }

        public Vector2 Speed { get; set; }

        public Vector2 ScreenPosition { get; set; }

        public virtual void Update(GameTime time)
        {
            WorldPosition += Speed * (float)time.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), Color.White);
        }
    }
}
