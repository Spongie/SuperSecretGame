using CVCommon;
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
        int indexToDraw;

        public EditorGrid(Vector2 piSize)
        {
            Init();
            screenSize = piSize;
            font = ContentHolder.LoadExtraContent<SpriteFont>("Font");
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

            PlatformTextures[PlatformType.CastleFloor] = ContentHolder.LoadExtraContent<Texture2D>("CastleFloor.png");
            PlatformTextures[PlatformType.CastleWall] = ContentHolder.LoadExtraContent<Texture2D>("CastleWall.png");
        }

        public void Update()
        {
            Camera camera = CameraController.GetCamera();
            camera.UpdateEditorCamera(blockSize.ToVector2());
            Point mousePosition = KeyMouseReader.mouseState.Position;

            for (int i = 0; i < screenBlocks.Count; i++)
            {
                if (screenBlocks[i].Contains(mousePosition))
                {
                    indexToDraw = i;
                    break;
                }
            }

            if (Clicked())
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
        }


        private bool Clicked()
        {
            if(ClickMode)
                return KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && KeyMouseReader.oldMouseState.LeftButton == ButtonState.Released;

            return KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Camera camera = CameraController.GetCamera();

            foreach (Platform platform in map.Platforms)
            {
                Point screenPosition = camera.GetObjectScreenPosition(platform.PlatformSettings.Position.ToVector2()).ToPoint();
                platform.DrawEditor(spriteBatch, new Rectangle(screenPosition.X, screenPosition.Y, blockSize.X, blockSize.Y), PlatformTextures[platform.PlatformSettings.Platform_Type]);
            }

            DrawMouseOver(spriteBatch);
        }

        private void DrawMouseOver(SpriteBatch spriteBatch)
        {
            if (indexToDraw >= 0)
                spriteBatch.Draw(PlatformTextures[Platform_Type], screenBlocks[indexToDraw], Color.White);
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
