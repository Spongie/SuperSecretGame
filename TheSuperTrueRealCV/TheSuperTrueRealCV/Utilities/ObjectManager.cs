using CV_clone;
using CVCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperTrueRealCV.Utilities.Enums;

namespace TheSuperTrueRealCV.Utilities
{
    public static class ObjectManager
    {
        public static void Init()
        {
            Monsters = new List<Moving_Entity>();
            Platforms = new List<Entity>();
            Attacks = new List<Attack>();
        }

        public static Player player;
        public static List<Moving_Entity> Monsters { get; private set; }
        public static List<Entity> Platforms { get; private set; }
        public static List<Attack> Attacks { get; private set; }

        public static void ApplyPhysics(GameTime gameTime)
        {
            Camera camera = CameraController.GetCamera();

            foreach (var platform in Platforms.Where(plat => camera.IsInsideVeiwSpace(plat.ScreenRect)))
            {
                PlayerToPlatformCollision(platform);
                MonsterCollision(camera, platform);
            }

            if (!player.Movement_Restrictions.Down)
                player.Speed += new Vector2(0, Settings.gravityPower);

            recalculateScreenPositions();
        }

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            foreach (var monster in Monsters)
            {
                monster.Update(gameTime);
                monster.UpdateScreenPosition();
            }
            var attacksToRemove = new List<Attack>();

            foreach (var attack in Attacks)
            {
                attack.Update(gameTime);
                if (attack.ReadyToDestroy)
                    attacksToRemove.Add(attack);

                attack.UpdateScreenPosition();
            }

            foreach (var attack in attacksToRemove)
            {
                Attacks.Remove(attack);
            }
        }

        private static void MonsterCollision(Camera camera, Entity platform)
        {
            foreach (var monster in Monsters.Where(mon => camera.IsInsideVeiwSpace(mon.ScreenRect)))
            {
                monster.Speed += new Vector2(0, Settings.gravityPower);
                if (monster.IgnoreCollision)
                    continue;

                if (player.WorldRect.Intersects(monster.WorldRect))
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

        private static void PlayerToPlatformCollision(Entity platform)
        {
            if (platform.WorldRect.Intersects(player.WorldRect))
            {
                bool playerInXRange = IsInsideXRange(player, platform);

                if (playerInXRange)
                {
                    if (player.WorldPosition.Y + 1.5f <= platform.WorldPosition.Y)
                    {
                        player.Movement_Restrictions.Down = true;
                        player.WorldPosition = new Vector2(player.WorldPosition.X, platform.WorldPosition.Y - player.WorldRect.Height);
                    }
                    else if (player.WorldPosition.Y >= platform.WorldPosition.Y)
                    {
                        player.Movement_Restrictions.Up = true;
                        player.WorldPosition = new Vector2(player.WorldPosition.X, platform.WorldPosition.Y + platform.WorldRect.Height);
                    }
                }
                else
                {
                    if (player.WorldRect.Right >= platform.WorldRect.X && Math.Abs(player.WorldRect.Right - platform.WorldRect.X) < 10)
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
            else if (player.WorldRect.Bottom == platform.WorldPosition.Y)
                player.Movement_Restrictions.Down = true;
        }

        private static bool IsInsideXRange(Moving_Entity entity, Entity platform)
        {
            return platform.WorldRect.X < entity.Center.X && platform.WorldRect.Right > entity.Center.X;
        }

        private static void recalculateScreenPositions()
        {
            recalculateMonsterScreenPosition();
            recalculatePlatformScreenPosition();

            foreach (var attack in Attacks)
            {
                attack.UpdateScreenPosition();
            }

            player.UpdateScreenPosition();
        }

        private static void recalculateMonsterScreenPosition()
        {
            Camera camera = CameraController.GetCamera();

            foreach (var monster in Monsters)
            {
                monster.UpdateScreenPosition();
            }
        }

        private static void recalculatePlatformScreenPosition()
        {
            Camera camera = CameraController.GetCamera();

            foreach (var platform in Platforms)
            {
                platform.UpdateScreenPosition();
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

            foreach (var attack in Attacks)
            {
                attack.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
        }

        public static void RegisterAttack(Attack piAttack)
        {
            if(piAttack.AttackType == AttackType.FollowOwner)
                piAttack.Flip(player.Facing);
            Attacks.Add(piAttack);
        }

        public static void FlipAttacks(Direction piDirection)
        {
            foreach (var attack in Attacks.Where(attack => attack.Owner == player && attack.AttackType == AttackType.FollowOwner))
            {
                attack.Flip(piDirection);
            }
        }
    }
}
