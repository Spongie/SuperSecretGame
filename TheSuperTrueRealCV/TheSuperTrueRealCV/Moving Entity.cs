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
        protected int jumpPower;
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
            CurrentStats = new Stats();
            direction = Direction.Right;
            spell = new List<Spell>();
            Movement_Restrictions = new MovementRestrictions();
        }

        public Stats CurrentStats { get; set; }

        public Direction Facing
        {
            get { return direction; }
            set { direction = value; }
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
            
            if (Speed.X == 0)
            {
                if (Facing == Direction.Left)
                    spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                else
                    spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            else if (Facing == Direction.Left)
                spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[animationIndex], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            else if (Facing == Direction.Right)
                spriteBatch.Draw(texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), animationRectangles[animationIndex], Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }
    }
}
