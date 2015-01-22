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
    class Bat : Moving_Entity
    {
        Moving_Entity target;
        List<Action> AiList = new List<Action>();
        bool Newtimer = false;
        int randomNewState;
        Random random = new Random();
        public Timer AiTimer;

        public Bat(Vector2 position) 
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
            AiList.Add(() => UpdateIdle());
        }

        public void UpdateIdle()
        {
            if (Vector2.Distance(WorldPosition, target.WorldPosition) <= 200 && target.WorldPosition.X <= WorldPosition.X)
            {
                AiList.Add(() => UpdateDiveLeft());
                direction = Direction.Left;
            }
            else if (Vector2.Distance(WorldPosition, target.WorldPosition) <= 200 && target.WorldPosition.X >= WorldPosition.X)
            {
                AiList.Add(() => UpdateDiveRight());
                direction = Direction.Right;
            }
            AiList.RemoveAt(0);
        }

        public void UpdateDiveLeft()
        {
            //FIXA SÅ DEN FALLER NERÅT

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (Math.Abs(WorldPosition.Y - target.WorldPosition.Y) <= 100 || AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillLeft());
            }
            AiList.RemoveAt(0);
        }

        public void UpdateChillLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/NER

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
            }
            AiList.RemoveAt(0);
        }

        public void UpdateRiseLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/UP

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillLeft());
            }
            AiList.RemoveAt(0);
        }

        public void UpdateDiveRight()
        {
            //FIXA SÅ DEN FALLER NERÅT

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (Math.Abs(WorldPosition.Y - target.WorldPosition.Y) <= 100 || AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillRight());
            }
            AiList.RemoveAt(0);
        }

        public void UpdateChillRight()
        {
            //FIXA SPEED ÅT HÖGER/NER

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
            }
            AiList.RemoveAt(0);
        }

        public void UpdateRiseRight()
        {
            //FIXA SPEED ÅT HÖGER/UP

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer = new Timer(300);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillRight());
            }
            AiList.RemoveAt(0);
        }
    }
}
