using CV_clone;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CVCommon;
using CV_clone.Utilities;
using TheSuperTrueRealCV.Utilities;

namespace TheSuperTrueRealCV.EnemyAI
{
    class SkeletonArmor : Moving_Entity
    {
        Moving_Entity target;
        List<Action> AiList = new List<Action>();
        bool HaveAttacked = false;
        bool Newtimer = false;
        Random random = new Random();
        public Timer AiTimer;

        public SkeletonArmor(Vector2 position) 
            : base(ContentHolder.LoadExtraContent<Texture2D>("Test"), position, Settings.objectSize)
        {
            //FIX ANIMATION FRAMES

            CurrentStats.MaximumHealth = 100;
            CurrentStats.MaximumMana = 0;
            CurrentStats.Damage = 10;
            CurrentStats.Defense = 2;
            CurrentStats.MagicDamage = 0;
            CurrentStats.MagicDefense = 3;

            AiTimer = new Timer(0);
        }

        public override void Update(GameTime time)
        {
            AiTimer.Update(time);

            if (AiList.Count > 0)
            {
                AiList[0].Invoke();
            }

            base.Update(time);

            Speed *= new Vector2(0, 1);
        }

        public void TurnAroundCheck()
        {
            if (target.WorldPosition.X < WorldPosition.X && direction == Direction.Right)
            {
                AiList.Add(() => UpdateTurnAround());
            }
            else if (target.WorldPosition.X > WorldPosition.X && direction == Direction.Left)
            {
                AiList.Add(() => UpdateTurnAround());
            }
        }

        public void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(500);
                Newtimer = true;
            }
            if (AiTimer.Done)
            {
                Newtimer = false;
                if (direction == Direction.Right)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Right;
                }
                AiList.RemoveAt(0);
            }

        }

        public void Activate(Moving_Entity target)
        {
            this.target = target;

            if (target.WorldPosition.X <= WorldPosition.X)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
            AiList.Add(() => UpdateIdle());
        }

        public void Disable()
        {
            AiList.Clear();
        }

        public void UpdateIdle()
        {
            AiList.Add(() => UpdateGoForward());
            AiList.RemoveAt(0);
        }

        public void UpdateGoForward()
        {
            if (direction == Direction.Right)
            {
                Speed = new Vector2(10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(-10, Speed.Y);
            }

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(1000);
                Newtimer = true;
            }

            if (Math.Abs(WorldPosition.X - target.WorldPosition.X) <= 150)
            {
                AiTimer = new Timer(0);
                Newtimer = false;
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
            }


        }

        public void UpdateAttack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(600);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                ObjectManager.RegisterAttack(AttackCreator.CreateTestAttack(WorldPosition + new Vector2(100, 0), new Vector2(100, 100), this),this);

                AiTimer = new Timer(1000);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                HaveAttacked = false;

                TurnAroundCheck();

                AiList.Add(() => UpdateGoForward());
                AiList.RemoveAt(0);
            }

        }

    }
}
