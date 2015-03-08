using CVCommon;
using CVStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSuperTrueRealCV.Handlers
{
    public class MapHandler
    {
        private Dictionary<string, Map> ivMaps;

        public Map ActiveMap { get; set; }

        public MapHandler()
        {
            ivMaps = new Dictionary<string, Map>();
            LoadAllMaps();
            setMapAktiv("Nigger");
        }

        public void LoadAllMaps()
        {
            foreach (var mapFile in Directory.GetFiles("Maps", "*.cmp"))
            {
                Map map = ObjectSerializer.DeSerialize<Map>(mapFile);
                string mapName = Path.GetFileNameWithoutExtension(mapFile);

                map.DeNormalizePositions();

                ivMaps.Add(mapName, map);
            }
        }

        public void setMapAktiv(string piMapName)
        {
            ActiveMap = ivMaps[piMapName];
        }
    }
}
