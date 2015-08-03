namespace TalentTree
{
    public class SaveNode
    {
        public SaveNode() : this(0, 0)
        {
        }

        public SaveNode(long piNodeID, long piParentID)
        {
            NodeID = piNodeID;
            ParentID = piParentID;
        }

        public long NodeID { get; set; }
        public long ParentID { get; set; }
    }
}
