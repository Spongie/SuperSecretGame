using CVCommon;
using CVCommon.Camera;
using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace CVLevelEditor
{
    class EditorGrid : BlockSettings
    {
        Vector2 screenSize;
        Point blockSize;
        List<Rectangle> screenBlocks;
        SpriteFont font;
        Map map;
        Camera camera;
        int indexToDraw;
        Texture2D light;
        Point lastPosition;

        public EditorGrid(Vector2 piSize)
        {
            Init();
            screenSize = piSize;
            font = ContentHolder.LoadExtraContent<SpriteFont>("Font");
            light = ContentHolder.LoadTexture("PropLight");
            blockSize = Point.Zero;

            blockSize.X = (int)screenSize.X / 20;
            blockSize.Y = (int)screenSize.Y / 20;

            Settings.objectSize = blockSize.ToVector2();

            for (int i = 0; i < screenSize.X; i+= blockSize.X)
            {
                for (int j = 0; j < screenSize.Y; j+= blockSize.Y)
                {
                    screenBlocks.Add(new Rectangle(i, j, blockSize.X, blockSize.Y));
                }
            }
        }

        private void Init()
        {
            indexToDraw = -1;
            map = new Map();
            screenBlocks = new List<Rectangle>();
            map.Platforms = new List<Platform>();
            PlatformTextures = new Dictionary<PlatformType, Texture2D>();

            PlatformTextures[PlatformType.CastleFloor] = ContentHolder.LoadTexture("CastleFloor.png");
            PlatformTextures[PlatformType.CastleWall] = ContentHolder.LoadTexture("CastleWall.png");
        }

        public void Update(PlacingMode placingMode, float intensity)
        {
            camera = CameraController.GetCamera();
            camera.UpdateEditorCamera(blockSize.ToVector2());
            Point mousePosition = KeyMouseReader.mouseState.Position;

            if (placingMode == PlacingMode.Platforms)
            {
                for (int i = 0; i < screenBlocks.Count; i++)
                {
                    if (screenBlocks[i].Contains(mousePosition))
                    {
                        indexToDraw = i;
                        break;
                    }
                }
            }

            if (Clicked())
            {
                if (placingMode == PlacingMode.Platforms)
                    PlacePlatform(mousePosition);
                else if (placingMode == PlacingMode.Props)
                    PlaceProp(mousePosition, intensity);
            }

            lastPosition = mousePosition;
        }

        private void PlaceProp(Point mousePosition, float intensity)
        {
            map.Lights.Add(new Light(light, mousePosition.ToVector2(), blockSize.ToVector2(), intensity));
        }

        private void PlacePlatform(Point mousePosition)
        {
            Rectangle targetRect = screenBlocks.Where(block => block.Contains(mousePosition)).First();

            try
            {
                Platform existingPlatform = map.Platforms.Where(platform => camera.GetObjectScreenPosition(platform.PlatformSettings.Position.ToVector2()).ToPoint() == targetRect.Location).First();

                if (existingPlatform != null)
                    map.Platforms.Remove(existingPlatform);
            }
            catch { }

            if (targetRect != null)
                map.Platforms.Add(new Platform(camera.GetObjectWorldPosition(targetRect.Location.ToVector2()).ToPoint(), Platform_Type, Platform_Status, Collidable));
        }

        private bool Clicked()
        {
            if(ClickMode)
                return KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && KeyMouseReader.oldMouseState.LeftButton == ButtonState.Released;

            return KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBatch, PlacingMode placingMode)
        {
            Camera camera = CameraController.GetCamera();

            foreach (Platform platform in map.Platforms)
            {
                Point screenPosition = camera.GetObjectScreenPosition(platform.PlatformSettings.Position.ToVector2()).ToPoint();
                platform.DrawEditor(spriteBatch, new Rectangle(screenPosition.X, screenPosition.Y, blockSize.X, blockSize.Y), PlatformTextures[platform.PlatformSettings.Platform_Type]);
            }

            foreach (var light in map.Lights)
            {
                Point screenPosition = camera.GetObjectScreenPosition(light.WorldPosition).ToPoint();
                light.ScreenPosition = screenPosition.ToVector2();
                light.Draw(spriteBatch);
            }

            if (placingMode == PlacingMode.Platforms)
                DrawMouseOverPlatforms(spriteBatch);
            else if (placingMode == PlacingMode.Props)
                DrawMouseOverProps(spriteBatch);
        }

        private void DrawMouseOverPlatforms(SpriteBatch spriteBatch)
        {
            if (indexToDraw >= 0)
                spriteBatch.Draw(PlatformTextures[Platform_Type], screenBlocks[indexToDraw], Color.White);
        }

        private void DrawMouseOverProps(SpriteBatch spriteBatch)
        {
            if (indexToDraw >= 0)
                spriteBatch.Draw(light, new Rectangle(lastPosition.X, lastPosition.Y, (int)Settings.objectSize.X / 2, (int)Settings.objectSize.Y / 2), Color.White);
        }

        public Map GetMap()
        {
            return map;
        }

        public void LoadMap(Map piMap)
        {
            map.Platforms = piMap.Platforms;
            map.Name = piMap.Name;
        }

        public Dictionary<PlatformType, Texture2D> PlatformTextures { get; set; }

        public bool ClickMode { get; set; }
    }
}
