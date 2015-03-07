using System;
using Microsoft.Xna.Framework;
using CVCommon;
using CV_clone.Utilities;
using TheSuperTrueRealCV.Utilities;
using CVCommon.Utility;

namespace TheSuperTrueRealCV.EnemyAI
{
    class SkeletonArmor : Monster
    {
        public SkeletonArmor(Vector2 position) 
            : base(position)
        {
            //FIX ANIMATION FRAMES

            CurrentStats.MaximumHealth = 100;
            CurrentStats.MaximumMana = 0;
            CurrentStats.Damage = 10;
            CurrentStats.Defense = 2;
            CurrentStats.MagicDamage = 0;
            CurrentStats.MagicDefense = 3;
        }

        public override void TurnAroundCheck()
        {
            if (target.WorldPosition.X < WorldPosition.X && CurrentDirection == Direction.Right)
            {
                AiList.Add(() => UpdateTurnAround());
            }
            else if (target.WorldPosition.X > WorldPosition.X && CurrentDirection == Direction.Left)
            {
                AiList.Add(() => UpdateTurnAround());
            }
        }

        public override void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(500);
                Newtimer = true;
            }
            if (AiTimer.Done)
            {
                Newtimer = false;
                if (CurrentDirection == Direction.Right)
                {
                    CurrentDirection = Direction.Left;
                }
                else
                {
                    CurrentDirection = Direction.Right;
                }
                AiList.RemoveAt(0);
            }

        }

        public override void UpdateIdle()
        {
            AiList.Add(() => UpdateGoForward());
            AiList.RemoveAt(0);
        }

        public override void UpdateGoForward()
        {
            if (CurrentDirection == Direction.Right)
            {
                Speed = new Vector2(10, Speed.Y);
            }
            else if (CurrentDirection == Direction.Left)
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

        public override void UpdateAttack()
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
