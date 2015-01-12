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
    public class Moving_Entity : Entity
    {
        protected int hP;
        protected int currentHP;
        protected int mP;
        protected int currentMP;
        protected float dMG;
        protected float dEF;
        protected float mDMG;
        protected float mDEF;
        protected float luck;
        protected float other;
        protected bool applyGravity;
        protected int jumpPower;
        protected bool canMoveLeft;
        protected bool canMoveRight;
        protected bool isHanging;
        protected float speedchange;
        protected float timeSinceAnimationChange;
        protected float timeBetweenAnimations;
        protected int animationIndex;
        protected List<Rectangle> animationRectangles;
        protected List<Spell> spell;

        public Moving_Entity(Texture2D tex, Vector2 pos, Vector2 size)
            : base(tex, pos, size)
        {

            direction = Direction.Right;
            spell = new List<Spell>();
            currentHP = hP;
            currentMP = mP;
        }

        public Direction Facing
        {
            get { return direction; }
            set { direction = value; }
        }

        public float SpeedChange
        {
            get { return speedchange; }
        }

        public int CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value; }
        }

        public int MaxHP
        {
            get { return (int)hP; }
            set { hP = value; }
        }

        public int CurrentMP
        {
            get { return currentMP; }
            set { currentMP = value; }
                
        }

        public int MaxMP
        {
            get { return mP; }
            set { mP = value; }
        }

        public float MPPercentage
        {
            get { return currentMP / MaxMP; }
        }

        public float HPPercentage
        {
            get { return currentHP / MaxHP; }
        }

        public bool ApplyGravity
        {
            get { return applyGravity; }
            set { applyGravity = value; }
        }

        public bool IsFalling
        {
            get { return Speed.Y > 0.0f; }
        }

        public bool IsJumping
        {
            get { return Speed.Y < 0.0f; }
        }

        public bool CanMoveLeft
        {
            get { return canMoveLeft; }
            set { canMoveLeft = value; }
        }

        public bool CanMoveRight
        {
            get { return canMoveRight; }
            set { canMoveRight = value; }
        }

        public override void Update(GameTime time)
        {
            for (int i = 0; i < spell.Count; i++)
            {
                spell[i].Update(time);
            }

            if (Speed.X > 0)
                Facing = Direction.Right;
            else if (Speed.X < 0)
                Facing = Direction.Left;

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
            for (int i = 0; i < spell.Count; i++)
            {
                spell[i].Draw(spriteBatch);                
            }

            if (animationRectangles.Count == 0)
            {
                spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), Color.White);
            }
            if (Speed.X == 0)
            {
                if (Facing == Direction.Left)
                    spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                else
                    spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            else if (Facing == Direction.Left)
                spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[animationIndex], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            else if (Facing == Direction.Right)
                spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[animationIndex], Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }
    }
}
