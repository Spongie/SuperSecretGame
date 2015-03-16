using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CVCommon;
using CVCommon.Utility;
using System.Xml.Serialization;
using TheSuperTrueRealCV;

namespace CV_clone
{
    public class Moving_Entity : Entity
    {
        protected int jumpPower;
        protected bool isHanging;
        protected float speedchange;
        protected bool autoDirectionControl;
        protected AnimationController ivAnimationController;

        public Moving_Entity()
        {

        }

        public Moving_Entity(Texture2D tex, Vector2 pos, Vector2 size)
            : base(tex, pos, size)
        {
            autoDirectionControl = true;
            ApplyGravity = true;
            CurrentStats = new Stats();
            CurrentDirection = Direction.Right;
            Movement_Restrictions = new MovementRestrictions();
            jumpPower = 400;
            ivAnimationController = new AnimationController();
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

        [XmlIgnore]
        public Vector2 Speed { get; set; }

        public bool ApplyGravity { get; set; }

        public bool IgnoreCollision { get; set; }

        public bool IsFalling
        {
            get { return Speed.Y > 0.0f; }
        }

        public bool IsJumping
        {
            get;
            set;
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

            Speed = Movement_Restrictions.Apply(Speed);
            WorldPosition += Speed * (float)time.ElapsedGameTime.TotalSeconds;
            Movement_Restrictions.Reset();

            base.Update(time);

            ivAnimationController.Update(time);
        }

        public virtual void Jump()
        {
            Jump(jumpPower);
        }

        public virtual void Jump(int power)
        {
            Speed = new Vector2(Speed.X , -power);
            IsJumping = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ivAnimationController.CurrentAnimation != null)
            {
               Rectangle sourceFromAnimation = ivAnimationController.CurrentAnimation.getCurrentRectangle();
               if (CurrentDirection == Direction.Left)
                   spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), sourceFromAnimation, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
               else
                   spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), sourceFromAnimation, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            else
            {
                if (CurrentDirection == Direction.Left)
                    spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                else
                    spriteBatch.Draw(Texture, new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
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
