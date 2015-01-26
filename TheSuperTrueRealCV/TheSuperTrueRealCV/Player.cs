using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CVCommon;

namespace CV_clone
{
    public class Player : Moving_Entity
    {
        
        bool m_HasJumped;
        private Vector2 m_Velocity;
        private Rectangle attackBox;
        private float attackBoxDuration;

        public Player(Texture2D tex, Vector2 vec, Vector2 size)
            :base(tex,vec,size)
        {
            Speed = new Vector2(0, 2);
            attackBoxDuration = 1;
        }

        public override void Update(GameTime gameTime)
        {
            speedchange = 0;
            CheckForJump();
            Move();
            ApplyPhysics();

            base.Update(gameTime);
        }

        private void Move()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
            {
                speedchange = -2;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
            {
                speedchange = 2;
            }
        }

        private void CheckForJump()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) && !m_HasJumped)
            {
                Jump();
                m_HasJumped = true;
            }
        }

        private void ApplyPhysics()
        {
            //DUNNO WHAT DIS IS
            m_Velocity.X = speedchange;
            m_Velocity += Speed;
        }

        private void Attack()
        {
            int x = (int)WorldPosition.X-texture.Width;
            int y = (int)WorldPosition.Y+(texture.Height/2);
            if (direction == Direction.Left)
            {
                attackBox = new Rectangle((int)WorldPosition.X, y, texture.Width, texture.Height / 3);
            }
            else
            {
                attackBox = new Rectangle((int)x, y, texture.Width, texture.Height / 3);
            }
        }
    }
}
