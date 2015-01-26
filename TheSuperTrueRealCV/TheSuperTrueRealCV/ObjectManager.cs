using CV_clone;
using CVCommon;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TheSuperTrueRealCV
{
    public class ObjectManager
    {
        public ObjectManager()
        {
            Monsters = new List<Moving_Entity>();
            platforms = new List<Entity>();
        }
        public static Moving_Entity player;

        public List<Moving_Entity> Monsters { get; set; }
        public List<Entity> platforms { get; set; }

        public void ApplyPhysics(GameTime gameTime)
        {
            Camera camera = CameraController.GetCamera();
            player.Speed += new Vector2(0, Settings.gravityPower) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var platform in platforms.Where(plat => camera.IsInsideVeiwSpace(plat.ScreenRect)))
            {
                bool playerInXRange = IsInsideXRange(player, platform);
                if (platform.WorldRect.Intersects(player.WorldRect) && playerInXRange)
                {
                    if (player.WorldPosition.Y < platform.WorldPosition.Y)
                        player.Movement_Restrictions.Down = true;
                    else if (player.WorldPosition.Y > platform.WorldPosition.Y)
                        player.Movement_Restrictions.Up = true;
                }

                foreach (var monster in Monsters.Where(mon => camera.IsInsideVeiwSpace(mon.ScreenRect)))
                {
                    monster.Speed += new Vector2(0, Settings.gravityPower) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(monster.IgnoreCollision)
                        continue;
                    
                    bool isInXRange = IsInsideXRange(monster, platform);

                    if (platform.WorldRect.Intersects(monster.WorldRect) && isInXRange)
                    {
                        if (monster.WorldPosition.Y > platform.WorldPosition.X)
                            monster.Movement_Restrictions.Down = true;
                        else if (monster.WorldPosition.Y < platform.WorldPosition.X)
                            monster.Movement_Restrictions.Up = true;
                    }
                }
            }
        }

        private bool IsInsideXRange(Moving_Entity monster, Entity platform)
        {
            return platform.WorldRect.X <= monster.WorldPosition.X && platform.WorldRect.X + platform.WorldRect.Width >= monster.WorldPosition.X;
        }
    }
}
