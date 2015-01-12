using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CV_clone;
using CVCommon;

namespace TheSuperTrueRealCV
{
    class SkeletonBone: Entity
    {
        Vector2 m_velocity;
        public SkeletonBone(Texture2D tex, Vector2 vec, Vector2 size, Vector2 initialVelocity)
            :base(tex,vec,size)
        {
            m_velocity = initialVelocity;
        }

        private void ApplyPhysics()
        {
            m_velocity.Y -= Settings.gravityPower;

            WorldPosition += m_velocity;
        }

        public void Update()
        {
            ApplyPhysics();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
