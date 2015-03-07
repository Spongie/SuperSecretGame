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
                platform.UpdateScreenPosition();
            }

            if (!player.Movement_Restrictions.Down)
                player.Speed += new Vector2(0, Settings.gravityPower);

        }

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            UpdateMonsters(gameTime);
            UpdateAttacks(gameTime);
        }

        private static void UpdateAttacks(GameTime gameTime)
        {
            var attacksToRemove = new List<Attack>();
            foreach (var attack in Attacks)
            {
                attack.Update(gameTime);
                if (attack.ReadyToDestroy)
                    attacksToRemove.Add(attack);
            }

            foreach (var attack in attacksToRemove)
            {
                Attacks.Remove(attack);
            }
        }

        private static void UpdateMonsters(GameTime gameTime)
        {
            var monsterToKill = new List<Moving_Entity>();
            foreach (var monster in Monsters)
            {
                monster.Update(gameTime);
                if (!monster.IsAlive)
                    monsterToKill.Add(monster);
            }
            foreach (var monster in monsterToKill)
            {
                Monsters.Remove(monster);
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

                foreach (var attack in Attacks)
                {
                    if (attack.RealHitbox.Intersects(monster.WorldRect))
                        monster.DealDamage(DamageCalcualtor.CalculateDamage(attack.Owner.CurrentStats, monster.CurrentStats, attack.Scaling));

                    if(attack.RealHitbox.Intersects(player.WorldRect))
                        player.DealDamage(DamageCalcualtor.CalculateDamage(attack.Owner.CurrentStats, player.CurrentStats, attack.Scaling));
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

        public static void RegisterAttack(Attack piAttack, Moving_Entity owner)
        {
            if(piAttack.AttackType == AttackType.FollowOwner)
                piAttack.Flip(owner.Facing);

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
