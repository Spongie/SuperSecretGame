using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CVCommon
{
    public class Platform : Entity
    {
        public Platform() : base(null, Vector2.Zero, Settings.objectSize)
        {
            PlatformSettings = new BlockSettings();
        }

        public Platform(Point piPosition, PlatformType piPlatformType, PlatformStatus piPlatformStatus, bool piCollidable) : this()
        {
            PlatformSettings.Position = piPosition;
            PlatformSettings.Platform_Type = piPlatformType;
            PlatformSettings.Platform_Status = piPlatformStatus;
            PlatformSettings.Collidable = piCollidable;
            WorldPosition = piPosition.ToVector2();
            texture = ContentHolder.LoadExtraContent<Texture2D>("test");
        }

        public BlockSettings PlatformSettings { get; set; }

        public void DrawEditor(SpriteBatch spriteBatch, Rectangle rectangle, Texture2D texture)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
