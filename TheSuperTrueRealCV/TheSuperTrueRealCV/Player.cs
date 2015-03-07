using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CVCommon;
using TheSuperTrueRealCV.Utilities;
using CV_clone.Utilities;

namespace CV_clone
{
    public class Player : Moving_Entity
    {
        private Vector2 extraSpeed;

        public Player(Texture2D tex, Vector2 vec, Vector2 size)
            :base(tex,vec,size)
        {
            jumpPower = 600;
            CurrentStats.MaximumHealth = 100;
            CurrentStats.MaximumMana = 50;
            CurrentStats.CurrentMana = 50;
            CurrentStats.CurrentHealth = 100;
            CurrentStats.Damage = 1337;
            CurrentStats.MaximumExp = 125;
            CurrentStats.RewardExperience(16);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive)
                return;

            HandleInput();
            Direction preUpdate = Direction;
            base.Update(gameTime);

            if(!IsFalling && !IsJumping && !Controllable)
            {
                Controllable = true;
                Speed = Vector2.Zero;
            }
            if(Direction != preUpdate)
                ObjectManager.FlipAttacks(Direction);
            if(Controllable)
                Speed -= extraSpeed;
        }

        private void HandleInput()
        {
            if (!Controllable)
                return;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
                extraSpeed = new Vector2(-100, 0);
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
                extraSpeed = new Vector2(100, 0);
            else
                extraSpeed = Vector2.Zero;

            if (KeyMouseReader.KeyPressed(Keys.Q))
                ObjectManager.RegisterAttack(AttackCreator.CreateTestAttack(WorldPosition + new Vector2(100, 0), new Vector2(100, 100), this),this);

            if (KeyMouseReader.KeyPressed(Keys.Space) && CanJump)
                Jump();
            
            Speed += extraSpeed;
        }

        public bool CanJump
        {
            get { return !IsFalling && !IsJumping; }
        }

        public bool Controllable { get; set; }

    }
}
