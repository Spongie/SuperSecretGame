using CV_clone;
using CVCommon;
using CVCommon.Camera;
using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperTrueRealCV.EnemyAI;
using TheSuperTrueRealCV.Utilities.Enums;

namespace TheSuperTrueRealCV.Utilities
{
    public static class ObjectManager
    {
        public static void Init()
        {
            Monsters = new List<Monster>();
            Platforms = new List<Entity>();
            Attacks = new List<Attack>();
        }

        public static Player player;
        public static List<Monster> Monsters { get; private set; }
        public static List<Entity> Platforms { get; private set; }
        public static List<Attack> Attacks { get; private set; }

        public static void ApplyPhysics(GameTime gameTime)
        {
            Camera camera = CameraController.GetCamera();

            foreach (var platform in Platforms.Where(plat => camera.IsInsideUpdateSpace(plat.ScreenRect)))
            {
                PlayerToPlatformCollision(platform);
                MonsterCollision(camera, platform);

                foreach (var attack in Attacks)
                {
                    if(attack.RealHitbox.Intersects(platform.WorldRect))
                    {
                        if (attack.Bouncing)
                        {
                            if (IsInsideXRange(attack.RealHitbox, platform))
                            {
                                attack.WorldPosition += new Vector2(0, -1);
                                attack.Speed *= new Vector2(0.8f, -0.8f);
                            }
                            if (attack.RealHitbox.Right >= platform.WorldRect.X && Math.Abs(attack.RealHitbox.Right - platform.WorldRect.X) < 10)
                                attack.Speed *= new Vector2(-0.8f, -0.8f);

                            else if (platform.WorldRect.Right >= attack.RealHitbox.Left && Math.Abs(platform.WorldRect.Right - attack.RealHitbox.Left) < 10)
                                attack.Speed *= new Vector2(-0.8f, -0.8f);

                            attack.BouncesLeft--;
                        }
                        if (attack.DiesOnCollision)
                            attack.ReadyToDestroy = true;
                    }
                }

                platform.UpdateScreenPosition();
            }
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
            var monsterToKill = new List<Monster>();
            foreach (var monster in Monsters.Where(mon => CameraController.GetCamera().IsInsideUpdateSpace(mon.ScreenRect)))
            {
                if (!monster.IsActive)
                    monster.Activate(player);

                monster.Update(gameTime);
                if (!monster.IsAlive)
                    monsterToKill.Add(monster);
            }

            foreach (var monster in Monsters.Where(monster => !CameraController.GetCamera().IsInsideUpdateSpace(monster.ScreenRect)))
            {
                if (monster.IsActive)
                    monster.Disable();
            }

            foreach (var monster in monsterToKill)
            {
                Monsters.Remove(monster);
                player.CurrentStats.RewardExperience(monster.ExpReward);
            }
        }

        private static void MonsterCollision(Camera camera, Entity platform)
        {
            foreach (var monster in Monsters.Where(mon => camera.IsInsideUpdateSpace(mon.ScreenRect)))
            {
                if (monster.IgnoreCollision)
                    continue;

                if (player.WorldRect.Intersects(monster.WorldRect))
                {
                    player.Speed = new Vector2(0, -500);
                    player.Controllable = false;
                }

                if (platform.WorldRect.Intersects(monster.WorldRect))
                {
                    bool isInXRange = IsInsideXRange(monster.WorldRect, platform);

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
                        HandleAttackHit(attack, monster);
                    if (attack.RealHitbox.Intersects(player.WorldRect))
                        HandleAttackHit(attack, player);
                }
            }
        }

        private static void HandleAttackHit(Attack piAttack, Moving_Entity piTarget)
        {
            if (piAttack.CanHitEntity(piTarget))
            {
                piTarget.DealDamage(DamageCalcualtor.CalculateDamage(piAttack.Owner.CurrentStats, piTarget.CurrentStats, piAttack.Scaling));
                piAttack.EntitiesHit.Add(piTarget, piAttack.getHitResetTimer());

                if (piAttack.DiesOnCollision)
                    piAttack.ReadyToDestroy = true;
            }
        }

        private static void PlayerToPlatformCollision(Entity platform)
        {
            if (platform.WorldRect.Intersects(player.WorldRect))
            {
                bool playerInXRange = IsInsideXRange(player.WorldRect, platform);

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
                        player.Movement_Restrictions.Right = true;

                    else if (platform.WorldRect.Right >= player.WorldRect.Left && Math.Abs(platform.WorldRect.Right - player.WorldRect.Left) < 10)
                        player.Movement_Restrictions.Left = true;
                }
            }
            else if (player.WorldRect.Bottom == platform.WorldPosition.Y)
                player.Movement_Restrictions.Down = true;
        }

        private static bool IsInsideXRange(Rectangle hitbox, Entity platform)
        {
            return platform.WorldRect.X < hitbox.Center.X && platform.WorldRect.Right > hitbox.Center.X;
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
                piAttack.Flip(owner.CurrentDirection);

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
