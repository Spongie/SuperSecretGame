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
                randomNewState = random.Next(0, 8);

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
                    AiList.Add(() => UpdateAttack1());
                }
                else if (randomNewState == 4)
                {
                    AiList.Add(() => UpdateMoveBack());
                }
                else if (randomNewState == 5)
                {
                    AiList.Add(() => UpdateMoveForward());
                }
                else if (randomNewState == 6)
                {
                    AiList.Add(() => UpdateJumpBack());
                }
                else if (randomNewState == 7)
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
                HaveAttacked = false;
                Newtimer = false;
                AiList.Add(() => UpdateExhausted());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateMagicAttack1()
        {

        }

        public void UpdateAttack1()
        {

        }

        public void UpdateAttack2()
        {

        }

        public void UpdateMoveBack()
        {

        }

        public void UpdateMoveForward()
        {

        }

        public void UpdateJumpBack()
        {

        }

        public void UpdateJumpForward() 
        {

        }

        public void UpdateExhausted()
        {
 
        }
    }
}
