using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TalentTree
{
    public class TalentTree
    {
        private Dictionary<long, TalentNode> ivNodes;

        public TalentTree()
        {
            ivNodes = new Dictionary<long, TalentNode>();
            Construct();
        }

        private void Construct()
        {
            ivNodes.Add(1, new TalentNode(this, 1, 0, 0));
            ivNodes.Add(2, new TalentNode(this, 2, 1, 1));
            ivNodes.Add(3, new TalentNode(this, 3, 2, 2));
            ivNodes.Add(4, new TalentNode(this, 4, 0, 0));
            ivNodes.Add(5, new TalentNode(this, 5, 0, 2));
        }

        public TalentNode GetNode(long piID)
        {
            return ivNodes[piID];
        }

        public int HighestLevelLearned()
        {
            return ivNodes.Where(node => node.Value.IsLearned()).Max(node => node.Value.TreeLevel);
        }

        public bool IsParentLearned(long piParentID, long piNodeID)
        {
            int treeLevel = GetNode(piNodeID).TreeLevel;
            return piParentID == 0 && (HighestLevelLearned() >= treeLevel + 1 || treeLevel == 0);
        }

        public void Save(string basePath)
        {
            var saveString = new StringBuilder();
            foreach (var node in ivNodes.Values)
            {
                var nodeString = JsonConvert.SerializeObject(new SaveNode(node.NodeID, node.ParentID));
                saveString.AppendLine(nodeString);
            }

            File.WriteAllText(basePath + "\\talentData.ini", saveString.ToString());
        }

        public void Load(string basePath)
        {
            string[] nodeStrings = File.ReadAllLines(basePath + "\\talentData.ini");

            foreach (var nodeString in nodeStrings)
            {
                var saveNode = JsonConvert.DeserializeObject<SaveNode>(nodeString);
                //STUFF
            }
        }
    }
}
