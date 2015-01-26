using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
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
            m_Velocity += new Vector2(0,Settings.gravityPower);
            m_Velocity.X = speedchange;
            m_Velocity += Speed;
            WorldPosition += m_Velocity;
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
