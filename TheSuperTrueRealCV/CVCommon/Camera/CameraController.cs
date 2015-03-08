using Microsoft.Xna.Framework;

namespace CVCommon.Camera
{
    public class CameraController
    {
        static Camera camera;

        public static void InitCamera()
        {
            camera = new Camera(Vector2.Zero);
            camera.Style = Utilities.CameraStyle.Horizontal;
        }

        public static Camera GetCamera()
        {
            return camera;
        }


    }
}
