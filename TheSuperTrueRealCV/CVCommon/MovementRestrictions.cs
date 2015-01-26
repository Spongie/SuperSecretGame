using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCommon
{
    public class MovementRestrictions
    {
        public MovementRestrictions()
        {
            Reset();
        }

        public void Reset()
        {
            Left = false;
            Right = false;
            Up = false;
            Down = false;
        }

        public Vector2 Apply(Vector2 piSpeed)
        {
            if (Left && piSpeed.X < 0)
                piSpeed.X = 0;
            if (Right && piSpeed.X > 0)
                piSpeed.X = 0;
            if (Up && piSpeed.Y < 0)
                piSpeed.Y = 0;
            if (Down && piSpeed.Y > 0)
                piSpeed.Y = 0;

            return piSpeed;
        }

        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
    }
}
