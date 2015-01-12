using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace CV_clone
{
    class IDTimer : Timer
    {
        private int id;

        public IDTimer(int id, float ms)
            : base(ms)
        {
            this.id = id;
        }

        public int ID
        {
            get { return id; }
        }
    }
}
