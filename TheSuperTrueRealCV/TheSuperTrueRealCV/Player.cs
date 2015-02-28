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
        private bool doneLastFrame = false;
        public Player(Texture2D tex, Vector2 vec, Vector2 size)
            :base(tex,vec,size)
        {
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            base.Update(gameTime);

            if(!IsFalling && !IsJumping && !Controllable)
            {
                Controllable = true;
                Speed = Vector2.Zero;
            }

            if(Controllable)
                Speed -= extraSpeed;
        }

        private void HandleInput()
        {
            if (!Controllable)
                return;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
                extraSpeed = new Vector2(-50, 0);
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
                extraSpeed = new Vector2(50, 0);
            else
                extraSpeed = Vector2.Zero;

            Speed += extraSpeed;
        }

        public Stats Stats { get; set; }

        public bool Controllable { get; set; }

    }
}
