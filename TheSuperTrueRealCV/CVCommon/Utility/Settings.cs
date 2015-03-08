﻿using Microsoft.Xna.Framework;

namespace CVCommon.Utility
{
    public static class Settings
    {
        public static Vector2 gameSize;
        public static Vector2 playerSize;
        public static Vector2 objectSize;
        public static float gravityPower;

        public static void SetDefaults()
        {
            gameSize = new Vector2(800, 640);
            gravityPower = 100;
            playerSize = new Vector2(175, 125);
            objectSize = new Vector2(50, 50);
        }

        public static Rectangle GetTriggerBox(Point p)
        {
            return new Rectangle(p.X, p.Y, (int)objectSize.X, (int)objectSize.Y);
        }
    }
}
