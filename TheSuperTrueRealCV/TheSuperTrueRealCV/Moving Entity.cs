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
using CVCommon.Utility;

namespace CV_clone
{
    public class Moving_Entity : Entity
    {
        protected int jumpPower;
        protected bool isHanging;
        protected float speedchange;
        protected float timeSinceAnimationChange;
        protected float timeBetweenAnimations;
        protected int animationIndex;
        protected List<Rectangle> animationRectangles;
        protected List<Spell> spell;
        protected bool autoDirectionControl;

        public Moving_Entity(Texture2D tex, Vector2 pos, Vector2 size)
            : base(tex, pos, size)
        {
            autoDirectionControl = true;
            ApplyGravity = true;
            CurrentStats = new Stats();
            CurrentDirection = Direction.Right;
            spell = new List<Spell>();
            Movement_Restrictions = new MovementRestrictions();
            jumpPower = 400;
        }

        public Stats CurrentStats { get; set; }

        public Direction CurrentDirection { get; set; }

        public bool IsAlive
        {
            get { return CurrentStats.CurrentHealth > 0; }
        }

        public float SpeedChange
        {
            get { return speedchange; }
        }

        public bool ApplyGravity { get; set; }

        public bool IgnoreCollision { get; set; }

        public bool IsFalling
        {
            get { return Speed.Y > 0.0f; }
        }

        public bool IsJumping
        {
            get { return Speed.Y < 0.0f; }
        }

        public override void Update(GameTime time)
        {
            if(ApplyGravity && !Movement_Restrictions.Down)
                Speed += new Vector2(0, Settings.gravityPower);

            if (autoDirectionControl)
            {
                if (Speed.X > 0)
                    CurrentDirection = Direction.Right;
                else if (Speed.X < 0)
                    CurrentDirection = Direction.Left;
            }

            base.Update(time);
        }

        public virtual void Jump()
        {
            Speed = new Vector2(Speed.X,  -jumpPower);
        }

        public virtual void Jump(int power)
        {
            Speed = new Vector2(Speed.X , -power);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentDirection == Direction.Left)
                spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            else
                spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }

        public void DealDamage(float amount)
        {
            CurrentStats.DealDamage(amount);
        }

        public void DrainMana(int amount)
        {
            CurrentStats.DrainMana(amount);
        }
    }
}
