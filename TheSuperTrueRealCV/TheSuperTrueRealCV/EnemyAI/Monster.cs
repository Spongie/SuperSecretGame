using CV_clone;
using CV_clone.Utilities;
using CVCommon;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSuperTrueRealCV.EnemyAI
{
    public abstract class Monster : Moving_Entity
    {
        protected Moving_Entity target;
        protected List<Action> AiList;
        protected Random random;
        protected Timer AiTimer;
        protected bool HaveAttacked;
        protected bool Newtimer;
        protected int newState;

        public Monster(Vector2 position)
            : base(ContentHolder.LoadTexture("Test"), position, Settings.objectSize)
        {
            AiList = new List<Action>();
            random = new Random();
            HaveAttacked = false;
            Newtimer = false;
            AiTimer = new Timer(0);
        }

        public void Activate(Moving_Entity target)
        {
            this.target = target;

            if (target.WorldPosition.X <= WorldPosition.X)
                direction = Direction.Left;
            else
                direction = Direction.Right;
            
            AiList.Add(() => UpdateIdle());
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

        public void Disable()
        {
            AiList.Clear();
        }

        public bool IsActive
        {
            get { return AiList.Count > 0; }
        }

        public abstract void TurnAroundCheck();
        public abstract void UpdateTurnAround();
        public abstract void UpdateIdle();
        public abstract void UpdateGoForward();
        public abstract void UpdateAttack();

    }
}
