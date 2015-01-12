using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CVCommon
{
    public class Map
    {
        public Map()
        {
            Platforms = new List<Platform>();
            Name = "UNNAMEDMAP";
        }

        /// <summary>
        /// Used by the editor to normalize the platforms(set positions to 0,0, 1,0) to work on any size when loading
        /// </summary>
        public void NormalizePlatforms()
        {
            foreach (var platform in Platforms)
            {
                platform.PlatformSettings.Position /= new Point((int)Settings.objectSize.X, 1);
                platform.PlatformSettings.Position /= new Point(1, (int)Settings.objectSize.Y);
            }
        }

        /// <summary>
        /// Used by the editor to normalize the platforms(set positions to 0,0, 1,0) to work on any size when loading
        /// </summary>
        public void DeNormalizePlatforms()
        {
            foreach (var platform in Platforms)
            {
                platform.PlatformSettings.Position *= new Point((int)Settings.objectSize.X, 1);
                platform.PlatformSettings.Position *= new Point(1, (int)Settings.objectSize.Y);
            }
        }

        public string Name { get; set; }

        public List<Platform> Platforms { get; set; }
    }
}
