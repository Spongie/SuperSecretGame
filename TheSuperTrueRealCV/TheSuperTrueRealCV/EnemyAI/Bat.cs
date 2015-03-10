using System;
using Microsoft.Xna.Framework;
using CVCommon;
using CV_clone.Utilities;
using CVCommon.Utility;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Bat : Monster
    {
        public Bat(Vector2 position) 
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
            this.ApplyGravity = false;
            if (Vector2.Distance(WorldPosition, target.WorldPosition) <= 200 && target.WorldPosition.X <= WorldPosition.X)
            {
                AiList.Add(() => UpdateDiveLeft());
                CurrentDirection = Direction.Left;
                AiList.RemoveAt(0);
            }
            else if (Vector2.Distance(WorldPosition, target.WorldPosition) <= 200 && target.WorldPosition.X >= WorldPosition.X)
            {
                AiList.Add(() => UpdateDiveRight());
                CurrentDirection = Direction.Right;
                AiList.RemoveAt(0);
            }
        }

        public void UpdateDiveLeft()
        {
            //FIXA SÅ DEN FALLER NERÅT
            this.ApplyGravity = true;

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(1500);
                Newtimer = true;
            }

            if (Math.Abs(WorldPosition.Y - target.WorldPosition.Y) <= 5 || AiTimer.Done)
            {
                AiTimer = new Timer(0);
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateChillLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/NER
            Speed = new Vector2(-100, Speed.Y);


            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(350);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateRiseLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/UP
            Speed = new Vector2(-100, -75);
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateDiveRight()
        {
            //FIXA SÅ DEN FALLER NERÅT
            this.ApplyGravity = true;
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(1500);
                Newtimer = true;
            }

            if (Math.Abs(WorldPosition.Y - target.WorldPosition.Y) <= 5 || AiTimer.Done)
            {
                AiTimer = new Timer(0);
                Newtimer = false;
                AiList.Add(() => UpdateRiseRight());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateChillRight()
        {
            //FIXA SPEED ÅT HÖGER/NER
            Speed = new Vector2(100, Speed.Y);
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(350);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseRight());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateRiseRight()
        {
            //FIXA SPEED ÅT HÖGER/UP
            Speed = new Vector2(100, -75);
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillRight());
                AiList.RemoveAt(0);
            }
        }

        public override void TurnAroundCheck()
        {
        }

        public override void UpdateTurnAround()
        {
        }

        public override void UpdateGoForward()
        {
        }

        public override void UpdateAttack()
        {
        }
    }
}
