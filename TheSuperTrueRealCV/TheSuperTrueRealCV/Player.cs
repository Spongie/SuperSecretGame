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
    class Player : Moving_Entity
    {
        bool m_HasJumped;
        private Vector2 m_Velocity;
        public Player(Texture2D tex, Vector2 vec, Vector2 size)
            :base(tex,vec,size)
        {
            Speed = new Vector2(0, 2);
        }

        public void Update()
        {
            Speed = new Vector2();
            CheckForJump();
            Move();
            ApplyPhysics();
        }

        private void Move()
        {
            if (KeyMouseReader.KeyPressed(Keys.A)|| canMoveLeft)
            {
                m_Velocity.X -= 2;
            }
            else if (KeyMouseReader.KeyPressed(Keys.D))
            {
                m_Velocity += new Vector2(2,0);
            }
        }

        private void CheckForJump()
        {
            if (KeyMouseReader.KeyPressed(Keys.Space) && !m_HasJumped)
            {
                Jump();
                m_HasJumped = true;
            }
        }

        private void ApplyPhysics()
        {
            m_Velocity += new Vector2(0,Settings.gravityPower);

            WorldPosition += m_Velocity;
        }
    }
}
