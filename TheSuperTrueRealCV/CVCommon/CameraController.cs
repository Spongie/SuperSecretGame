﻿using Microsoft.Xna.Framework;

namespace CVCommon
{
    public class CameraController
    {
        static Camera camera;

        public static void InitCamera()
        {
            camera = new Camera(Vector2.Zero);
        }

        public static Camera GetCamera()
        {
            return camera;
        }
    }
}
