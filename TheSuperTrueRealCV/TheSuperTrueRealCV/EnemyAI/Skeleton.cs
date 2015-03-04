using CV_clone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CVCommon;
using CV_clone.Utilities;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Skeleton : Moving_Entity
    {
        Moving_Entity target;
        List<Action> AiList = new List<Action>();
        bool HaveAttacked = false;
        bool Newtimer = false;
        int randomNewState;
        Random random = new Random();
        public Timer AiTimer;

        public Skeleton(Vector2 position) 
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
            AiList.Add( ()=> UpdateIdle() );
        }

        public void Disable()
        {
            AiList.Clear();            
        }

        public void UpdateIdle() 
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
                randomNewState = random.Next(0, 6);

                if (randomNewState == 1 || randomNewState == 2)
                {
                    AiList.Add(() => UpdateGoBack());
                }

                else if (randomNewState == 3 || randomNewState == 4)
                {
                    AiList.Add(() => UpdateGoforward());
                }

                else if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }

                else if (randomNewState == 5)
                {
                    AiList.Add(() => UpdateAttack1());
                }
                AiList.RemoveAt(0);
            }
        }

        public void UpdateAttack1()
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
               // Speed = new Vector2(2000, 0);
                //skapar själva anfallet!
                //SkeletonBone attack = new SkeletonBone(bone, pos, new Vector2(50, 50), new Vector2(4, 4));
            }

            if (AiTimer.Done)
            {
                HaveAttacked = false;
                Newtimer = false;
                Speed = Vector2.Zero;
                TurnAroundCheck();
                randomNewState = random.Next(0, 5);

                if (randomNewState == 1 || randomNewState == 2)
                {
                    AiList.Add(() => UpdateGoBack());
                }
                else if (randomNewState == 3 || randomNewState == 4)
                {
                    AiList.Add(() => UpdateGoforward());
                }
                else if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }
                AiList.RemoveAt(0);
            }
        }

        public void UpdateGoforward()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(random.Next(600, 1200));
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (direction == Direction.Right)
            {
                Speed = new Vector2(10, Speed.Y);                
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(-10, Speed.Y); 
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack1());
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
            if (direction == Direction.Right)
            {
                Speed = new Vector2(-10, Speed.Y);
            }
            else if (direction == Direction.Left)
            {
                Speed = new Vector2(10, Speed.Y);
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack1());
                AiList.RemoveAt(0);
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

    }
}
