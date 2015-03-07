using Microsoft.Xna.Framework;

namespace CVCommon.Utility
{
    public class BlockSettings
    {
        public Point Position { get; set; }

        public PlatformType Platform_Type { get; set; }

        public PlatformStatus Platform_Status { get; set; }

        public bool Collidable { get; set; }
    }
}
