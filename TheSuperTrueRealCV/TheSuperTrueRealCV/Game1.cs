using CV_clone;
using CVCommon;
using CVCommon.Camera;
using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheSuperTrueRealCV.EnemyAI;
using TheSuperTrueRealCV.Handlers;
using TheSuperTrueRealCV.Interface;
using TheSuperTrueRealCV.Utilities;

namespace TheSuperTrueRealCV
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Skeleton skeleton;

        Platform platform;
        Platform platform2;
        Platform platform3;

        Player player;
        PlayerPortrait pp;

        GameHandler gameHandler;

        private RenderTarget2D gameRenderTarget;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            ContentHolder.Init(Content);
            CameraController.InitCamera();
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D s = Content.Load<Texture2D>("Test.png");
            Settings.SetDefaults();
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            //skeleton = new Skeleton(new Vector2(100, 50));

            //platform = new Platform(new Point(0, 300), PlatformType.CastleFloor, PlatformStatus.Normal, true);

            //platform2 = new Platform(new Point(0, 100), PlatformType.CastleFloor, PlatformStatus.Normal, true);
            //platform2.Size = new Vector2(50, 200);

            //platform3 = new Platform(new Point(600, 100), PlatformType.CastleFloor, PlatformStatus.Normal, true);
            //platform3.Size = new Vector2(50, 200);

            //player = new Player(s, new Vector2(551, 200), Settings.objectSize);
            //pp = new PlayerPortrait();
            //skeleton.Activate(player);
            //ObjectManager.Init();
            //ObjectManager.player = player;
            //ObjectManager.Monsters.Add(skeleton);
            //ObjectManager.Platforms.Add(platform);
            //ObjectManager.Platforms.Add(platform2);
            //ObjectManager.Platforms.Add(platform3);
            gameHandler = new GameHandler();
            gameRenderTarget = new RenderTarget2D(GraphicsDevice, 800, 600);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //ObjectManager.ApplyPhysics(gameTime);
            KeyMouseReader.Update();
            //platform.Size = new Vector2(2000, 1000);
            //ObjectManager.Update(gameTime);
            gameHandler.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            DrawFirstPass();
            DrawSecondPass();

            base.Draw(gameTime);
        }

        private void DrawSecondPass()
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(gameRenderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            spriteBatch.End();
        }

        private void DrawFirstPass()
        {
            GraphicsDevice.SetRenderTarget(gameRenderTarget);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            gameHandler.Draw(spriteBatch);
            //ObjectManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
