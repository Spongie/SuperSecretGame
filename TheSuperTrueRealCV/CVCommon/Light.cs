using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CVCommon
{
    public class Light : Entity
    {
        public Light(Texture2D texture, Vector2 position, Vector2 size, float intensity) : base(texture, position, size)
        { }

        public Light()
            : base(ContentHolder.tmp, Vector2.Zero, Settings.objectSize)
        { }


        public float Intensity { get; set; }
    }
}
