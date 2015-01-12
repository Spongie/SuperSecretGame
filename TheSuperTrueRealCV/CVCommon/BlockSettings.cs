using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVCommon
{
    public class BlockSettings
    {
        public Point Position { get; set; }

        public PlatformType Platform_Type { get; set; }

        public PlatformStatus Platform_Status { get; set; }

        public bool Collidable { get; set; }
    }
}
