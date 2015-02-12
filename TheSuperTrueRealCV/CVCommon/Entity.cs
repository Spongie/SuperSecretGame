using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

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

        public Vector2 Center
        {
            get { return new Vector2(WorldPosition.X + (Size.X / 2), WorldPosition.Y + (Size.Y / 2)); }
        }

        public Rectangle Rect
        {
            get { return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y); }
        }

        public Rectangle ScreenRect
        {
            get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y); }
        }

        public Rectangle WorldRect
        {
            get { return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y); }
        }

        public void UpdateScreenPosition()
        {
            ScreenPosition = CameraController.GetCamera().GetObjectScreenPosition(WorldPosition);
        }

        public Vector2 WorldPosition { get; set; }

        [XmlIgnore]
        public Vector2 Size { get; set; }

        [XmlIgnore]
        public Vector2 Speed { get; set; }

        [XmlIgnore]
        public Vector2 ScreenPosition { get; set; }

        [XmlIgnore]
        public MovementRestrictions Movement_Restrictions { get; set; }

        public virtual void Update(GameTime time)
        {
            Speed = Movement_Restrictions.Apply(Speed);
            WorldPosition += Speed * (float)time.ElapsedGameTime.TotalSeconds;
            Movement_Restrictions.Reset();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), Color.White);
        }
    }
}
