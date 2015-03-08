using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using CVCommon;
using CVCommon.Utility;
using CVCommon.Camera;

namespace CVLevelEditor
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EditorGrid editorGrid;
        LevelOptions lvlOptions;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Settings.SetDefaults();

            lvlOptions = new LevelOptions();
            lvlOptions.onMapLoaded += lvlOptions_onMapLoaded;
            lvlOptions.Show();
            IsMouseVisible = true;

            ContentHolder.InitOnlyContentManager(Content);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            editorGrid = new EditorGrid(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            CameraController.InitCamera();
        }

        void lvlOptions_onMapLoaded(object sender, System.EventArgs e)
        {
            editorGrid.LoadMap(lvlOptions.GameMap);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            SetEditorGridValues();

            editorGrid.Update(lvlOptions.Placing_Mode, lvlOptions.LightIntensity, lvlOptions.SelectedSpawn);
            base.Update(gameTime);
            lvlOptions.GameMap = editorGrid.GetMap();
        }

        private void SetEditorGridValues()
        {
            editorGrid.Platform_Type = lvlOptions.SelectedPlatformType;
            editorGrid.Platform_Status = lvlOptions.SelectedPlatformStatus;
            editorGrid.Collidable = lvlOptions.CollisionEnabled;
            editorGrid.ClickMode = lvlOptions.ClickMode;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            Texture2D enemyTexture = null;
            if(lvlOptions.SelectedSpawn != string.Empty)
                enemyTexture = ContentHolder.LoadTexture("Monsters\\"+lvlOptions.SelectedSpawn);

            editorGrid.Draw(spriteBatch, lvlOptions.Placing_Mode, enemyTexture);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
