using Microsoft.Xna.Framework;
using CVCommon;
using CV_clone.Utilities;
using TheSuperTrueRealCV.Utilities;
using CVCommon.Utility;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Skeleton : Monster
    {
        public Skeleton(Vector2 position) 
            : base(position)
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

        public override void UpdateIdle() 
        {
            //är idle så den gör inget
            if(AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(400, 1500));
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0, 6);

                if (newState == 1 || newState == 2)
                {
                    AiList.Add(() => UpdateGoBack());
                }

                else if (newState == 3 || newState == 4)
                {
                    AiList.Add(() => UpdateGoForward());
                }

                else if (newState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }

                else if (newState == 5)
                {
                    AiList.Add(() => UpdateAttack());
                }
                AiList.RemoveAt(0);
            }
        }

        public override void UpdateAttack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(200);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                //ny timer som avgör när han är färdig med sitt anfall och ska gå vidare med sin AI.
                AiTimer = new Timer(500);

                int xSpeed = CurrentDirection == Direction.Left ? -200 : 200;
                ObjectManager.RegisterAttack(AttackCreator.CreateSkeletonAttack(WorldPosition, this, new Vector2(xSpeed, -600)), this);
            }

            if (AiTimer.Done)
            {
                HaveAttacked = false;
                Newtimer = false;
                Speed = Vector2.Zero;
                TurnAroundCheck();
                newState = random.Next(0, 5);

                if (newState == 1 || newState == 2)
                {
                    AiList.Add(() => UpdateGoBack());
                }
                else if (newState == 3 || newState == 4)
                {
                    AiList.Add(() => UpdateGoForward());
                }
                else if (newState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }
                AiList.RemoveAt(0);
            }
        }

        public override void UpdateGoForward()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(600, 1200));
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (CurrentDirection == Direction.Right)
            {
                Speed = new Vector2(10, Speed.Y);                
            }
            else if (CurrentDirection == Direction.Left)
            {
                Speed = new Vector2(-10, Speed.Y); 
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }
            
        }

        public void UpdateGoBack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(700, 1400));
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (CurrentDirection == Direction.Right)
            {
                Speed = new Vector2(-10, Speed.Y);
            }
            else if (CurrentDirection == Direction.Left)
            {
                Speed = new Vector2(10, Speed.Y);
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }

        }

        public override void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(4000);
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
    }
}
