using CV_clone;
using CVCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSuperTrueRealCV.Utilities
{
    public static class ObjectManager
    {
        public static void Init()
        {
            Monsters = new List<Moving_Entity>();
            Platforms = new List<Entity>();
        }

        public static Player player;

        public static List<Moving_Entity> Monsters { get; set; }
        public static List<Entity> Platforms { get; set; }

        public static void ApplyPhysics(GameTime gameTime)
        {
            Camera camera = CameraController.GetCamera();
            player.Speed += new Vector2(0, Settings.gravityPower);
            foreach (var platform in Platforms.Where(plat => camera.IsInsideVeiwSpace(plat.ScreenRect)))
            {
                if (platform.WorldRect.Intersects(player.WorldRect))
                {
                    bool playerInXRange = IsInsideXRange(player, platform);

                    if (playerInXRange)
                    {
                        if (player.WorldPosition.Y < platform.WorldPosition.Y)
                            player.Movement_Restrictions.Down = true;
                        else if (player.WorldPosition.Y > platform.WorldPosition.Y)
                            player.Movement_Restrictions.Up = true;
                    }
                    else
                    {
                        if(player.WorldRect.Right >= platform.WorldRect.X && Math.Abs(player.WorldRect.Right - platform.WorldRect.X) < 10)
                        {
                            player.WorldPosition += new Vector2(-1, 0);
                            player.Movement_Restrictions.Right = true;
                        }

                        else if (platform.WorldRect.Right > player.WorldRect.Left && Math.Abs(platform.WorldRect.Right - player.WorldRect.Left) < 10)
                        {
                            player.WorldPosition += new Vector2(1, 0);
                            player.Movement_Restrictions.Left = true;
                        }
                    }
                }

                foreach (var monster in Monsters.Where(mon => camera.IsInsideVeiwSpace(mon.ScreenRect)))
                {
                    monster.Speed += new Vector2(0, Settings.gravityPower);
                    if(monster.IgnoreCollision)
                        continue;

                    if(player.WorldRect.Intersects(monster.WorldRect))
                    {
                        player.Speed = new Vector2(0, -500);
                        player.Controllable = false;
                    }

                    if (platform.WorldRect.Intersects(monster.WorldRect))
                    {
                        bool isInXRange = IsInsideXRange(monster, platform);

                        if (isInXRange)
                        {
                            if (monster.WorldPosition.Y > platform.WorldPosition.X)
                                monster.Movement_Restrictions.Down = true;
                            else if (monster.WorldPosition.Y < platform.WorldPosition.X)
                                monster.Movement_Restrictions.Up = true;
                        }
                        else
                        {
                            if (monster.WorldRect.Right >= platform.WorldRect.X && Math.Abs(monster.WorldRect.Right - platform.WorldRect.X) < 10)
                            {
                                monster.WorldPosition += new Vector2(-1, 0);
                                monster.Movement_Restrictions.Right = true;
                            }

                            else if (platform.WorldRect.Right > monster.WorldRect.Left && Math.Abs(platform.WorldRect.Right - monster.WorldRect.Left) < 10)
                            {
                                monster.WorldPosition += new Vector2(1, 0);
                                monster.Movement_Restrictions.Left = true;
                            }
                        }
                    }
                }
            }

            recalculateScreenPositions();
        }

        private static bool IsInsideXRange(Moving_Entity entity, Entity platform)
        {
            return platform.WorldRect.X < entity.Center.X && platform.WorldRect.Right > entity.Center.X;
        }

        private static void recalculateScreenPositions()
        {
            recalculateMonsterScreenPosition();
            recalculatePlatformScreenPosition();

            player.ScreenPosition = CameraController.GetCamera().GetObjectScreenPosition(player.WorldPosition);
        }

        private static void recalculateMonsterScreenPosition()
        {
            Camera camera = CameraController.GetCamera();

            foreach (var monster in Monsters)
            {
                monster.ScreenPosition = camera.GetObjectScreenPosition(monster.WorldPosition);
            }
        }

        private static void recalculatePlatformScreenPosition()
        {
            Camera camera = CameraController.GetCamera();

            foreach (var platform in Platforms)
            {
                platform.ScreenPosition = camera.GetObjectScreenPosition(platform.WorldPosition);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var platform in Platforms)
            {
                platform.Draw(spriteBatch);
            }

            foreach (var monster in Monsters)
            {
                monster.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
        }

        public static void RegisterAttack(Attack piAttack)
        {

        }
    }
}
