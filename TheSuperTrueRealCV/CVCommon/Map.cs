using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CVCommon
{
    public class Map
    {
        public Map()
        {
            Platforms = new List<Platform>();
            Lights = new List<Light>();
            Name = "UNNAMEDMAP";
        }

        /// <summary>
        /// Used by the editor to normalize the platforms(set positions to 0,0, 1,0) to work on any size when loading
        /// </summary>
        public void NormalizePositions()
        {
            foreach (var platform in Platforms)
            {
                platform.PlatformSettings.Position /= new Point((int)Settings.objectSize.X, 1);
                platform.PlatformSettings.Position /= new Point(1, (int)Settings.objectSize.Y);
            }

            foreach (var light in Lights)
            {
                light.WorldPosition /= new Vector2(Settings.objectSize.X, 1);
                light.WorldPosition /= new Vector2(1, Settings.objectSize.Y);
            }
        }

        /// <summary>
        /// Used by the editor to normalize the platforms(set positions to 0,0, 1,0) to work on any size when loading
        /// </summary>
        public void DeNormalizePositions()
        {
            foreach (var platform in Platforms)
            {
                platform.PlatformSettings.Position *= new Point((int)Settings.objectSize.X, 1);
                platform.PlatformSettings.Position *= new Point(1, (int)Settings.objectSize.Y);
            }

            foreach (var light in Lights)
            {
                light.WorldPosition *= new Vector2(Settings.objectSize.X, 1);
                light.WorldPosition *= new Vector2(1, Settings.objectSize.Y);
            }
        }

        public string Name { get; set; }

        public List<Platform> Platforms { get; set; }

        public List<Light> Lights { get; set; }
    }
}
