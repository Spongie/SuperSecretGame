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
    class SkeletonAI : IEnemyAI
    {
        Moving_Entity Player;
        List<Action> AiList = new List<Action>();
        Texture2D bone;
        bool HaveAttacked = false;
        Vector2 pos;
        float speed = 0.5f;
        Direction direction;
        AIState state;
        int randomNewState;
        Random random = new Random();
        public Timer AiTimer;
        public void Activate(Moving_Entity target)
        {
            if (target.WorldPosition.X <= pos.X)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
            state = AIState.Idle;
            AiList.Add( ()=> UpdateIdle() );
        }

        public void Update()
        {
            AiList[0].Invoke();


/*          if (state == AIState.NotAktive)
                UpdateNotActive();
            else if (state == AIState.Idle)
                UpdateIdle();
            else if (state == AIState.Attack1)
                UpdateAttack1();
            else if (state == AIState.GoForward)
                UpdateGoforward();
            else if (state == AIState.GoBack)
                UpdateGoBack();
            else if (state == AIState.TurnAround)
                UpdateTurnAround();*/
        }

        public void Disable()
        {
            
        }

        public void UpdateNotActive()
        {
            AiList.RemoveAt(0);
            // just nu sätter jag den till idle direkt
            state = AIState.Idle;
            AiList.Add(() => UpdateIdle());
        }

        public void UpdateIdle()
        {
            AiTimer = new Timer(random.Next(400, 1500));
            //är idle så den gör inget

            if (AiTimer.Done)
            {
                //turnaroundcheck kollar om man ska vända sig och om man ska det så kollar den vilket håll man vänder sig
                TurnAroundCheck();
                randomNewState = random.Next(0, 6);

                if (randomNewState == 1 || randomNewState == 2)
                {
                    state = AIState.GoBack;
                    AiList.Add(() => UpdateGoBack());
                }
                else if (randomNewState == 3 || randomNewState == 4)
                {
                    state = AIState.GoForward;
                    AiList.Add(() => UpdateGoforward());
                }
                else if (randomNewState == 0)
                {
                    state = AIState.Idle;
                    AiList.Add(() => UpdateIdle());
                }
                else if (randomNewState == 5)
                {
                    state = AIState.Attack1;
                    AiList.Add(() => UpdateAttack1());
                }
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);

            }
        }

        public void UpdateAttack1()
        {
            //timer som avgör när anfallet skickas.
            AiTimer = new Timer(200);
            if (AiTimer.Done && HaveAttacked == false)
            {
                //gör så han inte anfaller igen under samma anfallsrunda
                HaveAttacked = true;
                //ny timer som avgör när han är färdig med sitt anfall och ska gå vidare med sin AI.
                AiTimer = new Timer(400);
                //skapar själva anfallet!
                SkeletonBone attack = new SkeletonBone(bone, pos, new Vector2(50, 50), new Vector2(4, 4));
            }

            if (AiTimer.Done)
            {
                //turnaroundcheck kollar om man ska vända sig och om man ska det så kollar den vilket håll man vänder sig
                TurnAroundCheck();
                //randomar vilken state den ska in i
                randomNewState = random.Next(0, 5);
                //sätter haveattacked till false igen så att man kan anfalla igen nästa gång han kommer till attack1
                HaveAttacked = false;

                if (randomNewState == 1 || randomNewState == 2)
                {
                    state = AIState.GoBack;
                    AiList.Add(() => UpdateGoBack());
                }
                else if (randomNewState == 3 || randomNewState == 4)
                {
                    state = AIState.GoForward;
                    AiList.Add(() => UpdateGoforward());
                }
                else if (randomNewState == 0)
                {
                    state = AIState.Idle;
                    AiList.Add(() => UpdateIdle());
                }
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
            }
        }

        public void UpdateGoforward()
        {
            //timer som säger hur länge den ska gå
            AiTimer = new Timer(random.Next(600, 1200));
            //avgör vilken riktning som den ska gå
            if (direction == Direction.Right)
            {
                pos.X += speed;                
            }
            else if (direction == Direction.Left)
            {
                pos.X -= speed;
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                state = AIState.Attack1;
                AiList.Add(() => UpdateAttack1());
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);
            }

        }

        public void UpdateGoBack()
        {
            //timer som säger hur länge den ska gå
            AiTimer = new Timer(random.Next(700, 1400));
            //avgör vilken riktning som den ska gå
            if (direction == Direction.Right)
            {
                pos.X -= speed;
            }
            else if (direction == Direction.Left)
            {
                pos.X += speed;
            }
            //ska röra sig i 1/4 speed av spelaren

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                state = AIState.Attack1;
                AiList.Add(() => UpdateAttack1());
                //tar bort staten man är i så att man kommer vidare till nästa state
                AiList.RemoveAt(0);

            }

        }

        public void UpdateTurnAround()
        {
            AiTimer = new Timer(500);
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
            if (Player.WorldPosition.X < pos.X && direction == Direction.Right)
            {
                AiList.Add(() => UpdateTurnAround());
            }
            else if (Player.WorldPosition.X > pos.X && direction == Direction.Left)
            {
                AiList.Add(() => UpdateTurnAround());
            }
        }

    }
}
