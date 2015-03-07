using Microsoft.Xna.Framework;

namespace CVCommon.Utility
{
    public static class XNAExtenstions
    {
        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
