using CV_clone;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CVCommon;
using CV_clone.Utilities;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Skeletonmusician : Moving_Entity
    {
        Moving_Entity target;
        List<Action> AiList = new List<Action>();
        bool HaveAttacked = false;
        bool Newtimer = false;
        bool HaveUsedSuperAttack = false;
        int randomNewState;
        Random random = new Random();
        public Timer AiTimer;

        public Skeletonmusician(Vector2 position) 
            : base(ContentHolder.LoadTexture("Test"), position, Settings.objectSize)
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
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.HealthPercentage <= 40)
            {
                Newtimer = false;
                AiList.Add(() => UpdateDeepGrave());
                AiList.RemoveAt(0);
            }
            //är idle så den gör inget
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(400, 800));
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                randomNewState = random.Next(0, 7);

                if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateMagicAttack1());
                }
                else if (randomNewState == 1)
                {
                    AiList.Add(() => UpdateAttack1());
                }
                else if (randomNewState == 2)
                {
                    AiList.Add(() => UpdateAttack2());
                }
                else if (randomNewState == 3)
                {
                    AiList.Add(() => UpdateMoveBack());
                }
                else if (randomNewState == 4)
                {
                    AiList.Add(() => UpdateMoveForward());
                }
                else if (randomNewState == 5)
                {
                    AiList.Add(() => UpdateJumpBack());
                }
                else if (randomNewState == 6)
                {
                    AiList.Add(() => UpdateJumpForward());
                }
                AiList.RemoveAt(0);
            }
        }

        public void UpdateDeepGrave()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                //bossen säger något coolt
                AiTimer = new Timer(1000);
                Newtimer = true;
            }

            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveUsedSuperAttack = true;
                HaveAttacked = true;
                //efter 6000 så har han laddat färdigt sin magic och själva hitboxen skapas
                AiTimer = new Timer(8000);
                //SKAPA SJÄLVA ANFALLET
            }

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                HaveAttacked = false;
                Newtimer = false;
                AiList.Add(() => UpdateExhausted());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateMagicAttack1()
        {
            if (AiTimer.Done && HaveAttacked == false)
            {
                //skickar sin fireball efter 1500
                AiTimer = new Timer(1700);
                //skapa fireball anfallet
                HaveAttacked = true;
            }
            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                AiList.Add(() => UpdateIdle());
                AiList.RemoveAt(0);
            }

        }

        public void UpdateAttack1()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(400);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                AiTimer = new Timer(600);
                //anfallet tar 600 att göra(kastar random instrument i en kast bana
                HaveAttacked = true;
            }
            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                AiList.Add(() => UpdateIdle());
                AiList.RemoveAt(0);
            }

        }

        public void UpdateAttack2()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(200);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                AiTimer = new Timer(500);
                //gör en stabb med sitt vapen och skickar ner en blixt framför sig
                //skapa anfallet
            }
            if(AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                AiList.Add(() => UpdateIdle());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateMoveBack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(700, 1000));
                Newtimer = true;
            }

            if (direction == Direction.Right)
            {
                Speed = new Vector2(-10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(10, Speed.Y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                randomNewState = random.Next(0,6);

                if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }
                else if (randomNewState == 1)
                {
                    AiList.Add(() => UpdateJumpBack());
                }
                else if (randomNewState == 2)
                {
                    AiList.Add(() => UpdateJumpForward());
                }
                else if (randomNewState == 3)
                {
                    AiList.Add(() => UpdateMagicAttack1());
                }
                else if (randomNewState == 4)
                {
                    AiList.Add(() => UpdateAttack1());
                }
                else if (randomNewState == 5)
                {
                    AiList.Add(() => UpdateAttack2());
                }
                AiList.RemoveAt(0);

            }
        }

        public void UpdateMoveForward()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(300, 600));
                Newtimer = true;
            }

            if (direction == Direction.Right)
            {
                Speed = new Vector2(10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(-10, Speed.Y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                randomNewState = random.Next(0, 6);

                if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }
                else if (randomNewState == 1)
                {
                    AiList.Add(() => UpdateJumpBack());
                }
                else if (randomNewState == 2)
                {
                    AiList.Add(() => UpdateJumpForward());
                }
                else if (randomNewState == 3)
                {
                    AiList.Add(() => UpdateMagicAttack1());
                }
                else if (randomNewState == 4)
                {
                    AiList.Add(() => UpdateAttack1());
                }
                else if (randomNewState == 5)
                {
                    AiList.Add(() => UpdateAttack2());
                }
                AiList.RemoveAt(0);

            }
        }

        public void UpdateJumpBack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(700, 1000));
                Newtimer = true;
            }
            //GLÖM INTE ATT GÖRA SÅ DEN HOPPAR ME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (direction == Direction.Right)
            {
                Speed = new Vector2(-10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(10, Speed.Y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack1());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateJumpForward() 
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(700, 1000));
                Newtimer = true;
            }
            //GLÖM INTE ATT GÖRA SÅ DEN HOPPAR ME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (direction == Direction.Right)
            {
                Speed = new Vector2(-10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(10, Speed.Y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack2());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateExhausted()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(8000);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                randomNewState = random.Next(0, 3);

                if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateAttack1());
                }
                else if (randomNewState == 1)
                {
                    AiList.Add(() => UpdateAttack2());
                }
                else if (randomNewState == 2)
                {
                    AiList.Add(() => UpdateMagicAttack1());
                }
                AiList.RemoveAt(0);
            }
 
        }
    }
}
