using CV_clone;
using CVCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        ObjectManager om;
        Platform platform;

        Player player;


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
            ContentHolder.InitOnlyContentManager(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D s = Content.Load<Texture2D>("Test.png");
            Settings.objectSize = new Vector2(50, 50);
            Settings.gravityPower = 30;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            Settings.gameSize = new Vector2(800, 600);

            skeleton = new Skeleton(new Vector2(100, 100));

            platform = new Platform(new Point(0, 300), PlatformType.CastleFloor, PlatformStatus.Normal, true);

            player = new Player(s, new Vector2(400, 200), Settings.objectSize);


            skeleton.Activate(player);

            om = new ObjectManager();
            ObjectManager.player = player;
            om.Monsters.Add(skeleton);
            om.platforms.Add(platform);
            CameraController.InitCamera();
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
            platform.Size = new Vector2(2000, 1000);
            om.ApplyPhysics(gameTime);

            player.ScreenPosition = player.WorldPosition;
            player.Update(gameTime);
            skeleton.Update(gameTime);
            skeleton.ScreenPosition = skeleton.WorldPosition;

            platform.ScreenPosition = platform.WorldPosition;

            KeyMouseReader.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            platform.Draw(spriteBatch);
            skeleton.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
