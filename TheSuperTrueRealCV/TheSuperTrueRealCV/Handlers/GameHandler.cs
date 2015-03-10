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

        public GameHandler()
        {
            ivMapHandler = new MapHandler();
            ObjectManager.Init();
            ObjectManager.player = new Player(ContentHolder.LoadTexture("test"), ivMapHandler.ActiveMap.PlayerSpawnPosition, Settings.playerSize);
            ObjectManager.SetPlatforms(ivMapHandler.ActiveMap.Platforms);
            ObjectManager.SpawnEnemies(ivMapHandler.ActiveMap.EnemySpawns);
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
            CameraController.GetCamera().Update(ObjectManager.player.WorldPosition, gameTime);
            ObjectManager.ApplyPhysics(gameTime);
            ObjectManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ObjectManager.Draw(spriteBatch);
        }
    }
}
