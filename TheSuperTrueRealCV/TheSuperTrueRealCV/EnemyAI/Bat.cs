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
            if (Vector2.Distance(WorldPosition, target.WorldPosition) <= 200)
            {

            }

        }


    }
}
