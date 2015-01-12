using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CVCommon
{
    public static class Resolution
    {
        static private GraphicsDeviceManager Device = null;

        static private int width = 800;
        static private int height = 600;
        static private int VirtualWidth = 1024;
        static private int VirtualHeight = 768;
        static private Matrix scaleMatrix;
        static private bool isFullScreen = false;
        static private bool isDirtyMatrix = true;

        static public void Init(ref GraphicsDeviceManager device)
        {
            width = device.PreferredBackBufferWidth;
            height = device.PreferredBackBufferHeight;
            Device = device;
            isDirtyMatrix = true;
            ApplyResolutionSettings();
        }

        static public Matrix getTransformationMatrix()
        {
            if (isDirtyMatrix) 
                RecreateScaleMatrix();
            
            return scaleMatrix;
        }

        static public void SetResolution(int Width, int Height, bool FullScreen)
        {
            width = Width;
            height = Height;

            isFullScreen = FullScreen;

           ApplyResolutionSettings();
        }

        static public void SetVirtualResolution(int Width, int Height)
        {
            VirtualWidth = Width;
            VirtualHeight = Height;

            isDirtyMatrix = true;
        }

       static private void ApplyResolutionSettings()
       {
           if (isFullScreen == false)
           {
               if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                   && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
               {
                   Device.PreferredBackBufferWidth = width;
                   Device.PreferredBackBufferHeight = height;
                   Device.IsFullScreen = isFullScreen;
                   Device.ApplyChanges();
               }
           }
           else
           {
               foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
               {
                   if ((dm.Width == width) && (dm.Height == height))
                   {
                       Device.PreferredBackBufferWidth = width;
                       Device.PreferredBackBufferHeight = height;
                       Device.IsFullScreen = isFullScreen;
                       Device.ApplyChanges();
                   }
               }
           }

           isDirtyMatrix = true;

           width =  Device.PreferredBackBufferWidth;
           height = Device.PreferredBackBufferHeight;
       }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        static public void BeginDraw()
        {
            FullViewport();
            Device.GraphicsDevice.Clear(Color.Black);
            ResetViewport();
            Device.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        static private void RecreateScaleMatrix()
        {
            isDirtyMatrix = false;
            scaleMatrix = Matrix.CreateScale(
                           (float)Device.GraphicsDevice.Viewport.Width / VirtualWidth,
                           (float)Device.GraphicsDevice.Viewport.Width / VirtualWidth,
                           1f);
        }


        static public void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = width;
            vp.Height = height;
            Device.GraphicsDevice.Viewport = vp;
        }

        /// <summary>
        /// Get virtual Mode Aspect Ratio
        /// </summary>
        /// <returns>aspect ratio</returns>
        static public float getVirtualAspectRatio()
        {
            return (float)VirtualWidth / (float)VirtualHeight;
        }

        static public void ResetViewport()
        {
            float targetAspectRatio = getVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = Device.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;
            
            if (height > Device.PreferredBackBufferHeight)
            {
                height = Device.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (Device.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (Device.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed)
            {
                isDirtyMatrix = true;
            }

            Device.GraphicsDevice.Viewport = viewport;
        }

        public static Vector2 GetVirtualResolution()
        {
            return new Vector2(VirtualWidth, VirtualHeight);
        }

        public static Vector2 GetScreenResolution()
        {
            return new Vector2(width, height);
        }
    }
}
