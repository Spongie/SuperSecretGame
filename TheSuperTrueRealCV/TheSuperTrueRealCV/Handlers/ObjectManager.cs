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
            Platforms = new List<Platform>();
            Attacks = new List<Attack>();
        }

        public static Player player;
        public static List<Monster> Monsters { get; set; }
        public static List<Platform> Platforms { get; private set; }
        public static List<Attack> Attacks { get; private set; }

        public static void ApplyPhysics(GameTime gameTime)
        {
            Camera camera = CameraController.GetCamera();

            foreach (var platform in Platforms.Where(plat => camera.IsInsideUpdateSpace(plat.WorldRect)))
            {
                platform.UpdateScreenPosition();

                if (!platform.PlatformSettings.Collidable)
                    continue;

                HandleEntityToPlatformCollision(player, platform);

                MonsterCollision(camera, platform);

                foreach (var attack in Attacks)
                {
                    if(attack.RealHitbox.Intersects(platform.WorldRect))
                    {
                        if (attack.Bouncing)
                        {
                            if (IsInsideXRangeFromCenter(attack.RealHitbox, platform))
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

            }
        }

        public static void SpawnEnemies(List<EnemySpawn> piSpawnPoints)
        {
            foreach (var enemySpawn in piSpawnPoints)
            {
                var monsterType = Type.GetType("TheSuperTrueRealCV.EnemyAI." + enemySpawn.EnemyName);
                Monster monster = (Monster)Activator.CreateInstance(monsterType, enemySpawn.WorldPosition);

                Monsters.Add(monster);
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
            foreach (var monster in Monsters.Where(mon => CameraController.GetCamera().IsInsideUpdateSpace(mon.WorldRect)))
            {
                if (!monster.IsActive)
                    monster.Activate(player);

                monster.Update(gameTime);
                if (!monster.IsAlive)
                    monsterToKill.Add(monster);
            }

            foreach (var monster in Monsters.Where(monster => !CameraController.GetCamera().IsInsideUpdateSpace(monster.WorldRect)))
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

        public static void SetPlatforms(List<Platform> piPlatforms)
        {
            foreach (var platform in piPlatforms)
            {
                if (platform.Texture == null)
                    platform.Texture = ContentHolder.LoadTexture(platform.TextureName);

                Platforms.Add(platform);
            }
        }

        private static void MonsterCollision(Camera camera, Entity platform)
        {
            foreach (var monster in Monsters.Where(mon => camera.IsInsideUpdateSpace(mon.WorldRect)))
            {
                if (monster.IgnoreCollision)
                    continue;

                if (player.WorldRect.Intersects(monster.WorldRect))
                {
                    player.Speed = new Vector2(0, -500);
                    player.Controllable = false;
                }

                HandleEntityToPlatformCollision(monster, platform);

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


        private static void HandleEntityToPlatformCollision(Moving_Entity piEntity, Entity piPlatform)
        {
            if (piEntity.BotttomRect.Intersects(piPlatform.TopRect))
            {
                piEntity.Movement_Restrictions.Down = true;
                if(piEntity.WorldRect.Bottom > piPlatform.WorldPosition.Y)
                    piEntity.WorldPosition = new Vector2(piEntity.WorldPosition.X, piPlatform.WorldPosition.Y - piEntity.Size.Y);

                piEntity.IsJumping = false;
            }

            if (piEntity.TopRect.Intersects(piPlatform.BotttomRect))
                piEntity.Movement_Restrictions.Up = true;

            if (piEntity.LeftRect.Intersects(piPlatform.RightRect))
                piEntity.Movement_Restrictions.Left = true;

            if (piEntity.RightRect.Intersects(piPlatform.LeftRect))
                piEntity.Movement_Restrictions.Right = true;
        }

        private static bool CheckStandingOnPlatform(Moving_Entity piEntity, Entity piPlatform)
        {
            if (piEntity.WorldRect.Bottom == piPlatform.WorldPosition.Y && IsInsideXRangeFromCenter(piEntity.WorldRect, piPlatform))
                return true;
            else if (piEntity.WorldRect.Bottom == piPlatform.WorldPosition.Y && IsInsideXRangeFromLeft(piEntity.WorldRect, piPlatform))
                return true;
            else if (piEntity.WorldRect.Bottom == piPlatform.WorldPosition.Y && IsInsideXRangeFromRight(piEntity.WorldRect, piPlatform))
                return true;

            return false;
        }

        private static bool IsInsideXRangeFromCenter(Rectangle hitbox, Entity platform)
        {
            return platform.WorldRect.X < hitbox.Center.X && platform.WorldRect.Right > hitbox.Center.X;
        }

        private static bool IsInsideXRangeFromLeft(Rectangle hitbox, Entity platform)
        {
            return platform.WorldRect.X < hitbox.Left && platform.WorldRect.Right > hitbox.Left;
        }

        private static bool IsInsideXRangeFromRight(Rectangle hitbox, Entity platform)
        {
            return platform.WorldRect.X < hitbox.Right && platform.WorldRect.Right > hitbox.Right;
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
