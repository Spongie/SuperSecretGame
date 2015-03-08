using CV_clone;
using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSuperTrueRealCV.EnemyAI;
using TheSuperTrueRealCV.Utilities;
using TheSuperTrueRealCV.Utilities.Enums;
using System.Linq;
using CVCommon;
using CVCommon.Camera;

namespace TheSuperTrueRealCV.Handlers
{
    public class GameHandler
    {
        PlayingState ivGameState;
        MapHandler ivMapHandler;
        Skeleton skeleton;

        public GameHandler()
        {
            ivMapHandler = new MapHandler();
            ObjectManager.Init();
            ObjectManager.player = new Player(ContentHolder.LoadTexture("test"), new Vector2(751, 450), Settings.playerSize);
            skeleton = new Skeleton(new Vector2(500, 430));
            ObjectManager.Monsters.Add(skeleton);
            ObjectManager.SetPlatforms(ivMapHandler.ActiveMap.Platforms);
        }

        public void Update(GameTime gameTime)
        {
            if (ivGameState == PlayingState.Playing)
                UpdatePlaying(gameTime);
            else if (ivGameState == PlayingState.InTransition)
                UpdateTransition(gameTime);
        }

        private void UpdateTransition(GameTime gameTime)
        {
            
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            CameraController.GetCamera().Update(ObjectManager.player.WorldPosition);
            ObjectManager.ApplyPhysics(gameTime);
            ObjectManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ObjectManager.Draw(spriteBatch);
        }
    }
}
