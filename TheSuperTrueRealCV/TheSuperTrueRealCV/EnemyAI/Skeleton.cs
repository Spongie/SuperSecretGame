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

namespace TheSuperTrueRealCV
{
    class Skeleton : Moving_Entity
    {
        Moving_Entity Player;
        List<Action> AiList = new List<Action>();
        bool HaveAttacked = false;
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
            CurrentStats.Luck = -1;
            CurrentStats.MagicDamage = 0;
            CurrentStats.MagicDefense = 3;
            CurrentStats.Luck = 1;

            AiTimer = new Timer(0);
        }

        public bool CurrentActionDone { get; set; }

        public void Activate(Moving_Entity target)
        {
            Player = target;

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

        public override void Update(GameTime time)
        {
            AiTimer.Update(time);

            if(AiList.Count > 0)
                AiList[0].Invoke();

            base.Update(time);

            Speed *= new Vector2(0, 1);
        }

        public void Disable()
        {
            
        }

        public void UpdateNotActive()
        {
            AiList.RemoveAt(0);
            // just nu sätter jag den till idle direkt
            AiList.Add(() => UpdateIdle());
        }

        public void UpdateIdle()
        {
            //är idle så den gör inget

            if (AiTimer.Done)
            {
                //turnaroundcheck kollar om man ska vända sig och om man ska det så kollar den vilket håll man vänder sig
                TurnAroundCheck();
                randomNewState = random.Next(0, 6);

                if (randomNewState == 1 || randomNewState == 2)
                    AiList.Add(() => UpdateGoBack());

                else if (randomNewState == 3 || randomNewState == 4)
                    AiList.Add(() => UpdateGoforward());

                else if (randomNewState == 0)
                    AiList.Add(() => UpdateIdle());

                else if (randomNewState == 5)
                    AiList.Add(() => UpdateAttack1());
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);

                AiTimer = new Timer(random.Next(400, 600));
            }
        }

        public void UpdateAttack1()
        {
            //timer som avgör när anfallet skickas.
            if (AiTimer.Done && HaveAttacked == false)
            {
                //gör så han inte anfaller igen under samma anfallsrunda
                HaveAttacked = true;
                //ny timer som avgör när han är färdig med sitt anfall och ska gå vidare med sin AI.
                AiTimer = new Timer(500);

                Speed = new Vector2(2000, 0);
                //skapar själva anfallet!
                //SkeletonBone attack = new SkeletonBone(bone, pos, new Vector2(50, 50), new Vector2(4, 4));
            }

            if (AiTimer.Done)
            {
                Speed = Vector2.Zero;
                //turnaroundcheck kollar om man ska vända sig och om man ska det så kollar den vilket håll man vänder sig
                TurnAroundCheck();
                //randomar vilken state den ska in i
                randomNewState = random.Next(0, 5);
                //sätter haveattacked till false igen så att man kan anfalla igen nästa gång han kommer till attack1
                HaveAttacked = false;

                if (randomNewState == 1 || randomNewState == 2)
                {
                    AiList.Add(() => UpdateGoBack());
                    AiTimer = new Timer(random.Next(400, 600));
                }
                else if (randomNewState == 3 || randomNewState == 4)
                {
                    AiList.Add(() => UpdateGoforward());
                    AiTimer = new Timer(random.Next(400, 600));
                }
                else if (randomNewState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                    AiTimer = new Timer(random.Next(400, 600));
                }
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
            }
        }

        public void UpdateGoforward()
        {
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
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack1());
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
                AiTimer = new Timer(random.Next(400, 600));
            }

        }

        public void UpdateGoBack()
        {
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
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack1());
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
                AiTimer = new Timer(random.Next(400, 600));

            }

        }

        public void UpdateTurnAround()
        {
            if (AiTimer.Done)
            {
                if (direction == Direction.Right)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Right;
                }
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
            }

        }

        public void TurnAroundCheck()
        {
            if (Player.WorldPosition.X < WorldPosition.X && direction == Direction.Right)
            {
                AiList.Add(() => UpdateTurnAround());
                AiTimer = new Timer(random.Next(400, 600));
            }
            else if (Player.WorldPosition.X > WorldPosition.X && direction == Direction.Left)
            {
                AiList.Add(() => UpdateTurnAround());
                AiTimer = new Timer(random.Next(400, 600));
            }
        }

    }
}
