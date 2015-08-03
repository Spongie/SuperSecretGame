namespace TalentTree
{
    public class TalentNode
    {
        private TalentTree ivHelper;

        public TalentNode(TalentTree piHelper, long piNodeID, long piParentID, int piTreeLevel)
        {
            ivHelper = piHelper;
            NodeID = piNodeID;
            ParentID = piParentID;
            TreeLevel = piTreeLevel;
        }

        public int TreeLevel { get; set; }

        public int Level { get; set; }

        public long ParentID { get; private set; }

        public long NodeID { get; private set; }

        public bool Upgrade()
        {
            if (Level >= 1)
                return false;

            Level++;
            return true;
        }

        public TalentNode GetParent()
        {
            if (ParentID == 0)
                return null;

            return ivHelper.GetNode(ParentID);
        }

        public bool CanLearn()
        {
            if (IsLearned())
                return false;

            if (ivHelper.IsParentLearned(ParentID, NodeID))
                return true;

            return ivHelper.GetNode(ParentID).IsLearned();
        }

        public bool IsLearned()
        {
            return Level > 0;
        }
    }
}
