using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using CVCommon;

namespace CV_clone
{
    public class Spell : Moving_Entity
    {
        protected Timer timer;
        protected float power;
        protected DamageType dmgtype;
        protected bool checkgravity;
        protected int lives;
        protected Vector2 targetSize;
        private Timer growTimer;
        public bool FullSize
        {
            get
            {
                float x = targetSize.X - Size.X;
                float y = targetSize.Y - Size.Y;
                if(x <= 1 && y == 1)
                    return true;
                else
                    return false;
            }
        }

        public Vector2 TargetSize
        {
            get { return targetSize; }
        }
        public Timer SpellTimer
        {
            get { return timer;}
            set { timer = value;}
        }
        public float Power
        {
            get { return power;}
        }
        public DamageType Type
        {
            get { return dmgtype;}
        }
        public bool CheckGravity
        {
            get { return checkgravity;}
        }
        public int Lives 
        { 
            get{ return lives; }
            set { lives = value; }
        }
 
        public Spell(Texture2D tex, Vector2 pos, Vector2 size, Timer timer, float power, DamageType dmgtype, Vector2 speed, bool checkgravity, int lives)
            :base(tex, pos, size)
        {

            this.timer = timer;
            this.power = power;
            this.dmgtype = dmgtype;
            this.Speed = speed;
            this.checkgravity = checkgravity;
            this.lives = lives;
        }

        public Spell(Texture2D tex, Vector2 pos, Vector2 size, Vector2 targetSize, Timer timer, float power, DamageType dmgtype, Vector2 speed, bool checkgravity, int lives)
            : base(tex, pos, size)
        {

            this.targetSize = targetSize;
            this.timer = timer;
            this.power = power;
            this.dmgtype = dmgtype;
            this.Speed = speed;
            this.checkgravity = checkgravity;
            this.lives = lives;
            growTimer = new Timer(12);
        }

        public override void Update(GameTime time)
        {
           if (checkgravity)
           {
               Speed += new Vector2(0, Settings.gravityPower);
           }
           else if (!checkgravity)
           {
               Speed = new Vector2(Speed.X, 0.0f);
           }

           if (timer.Done)
           {
               lives = 0;
           }

           base.Update(time);
        }
    }
}
