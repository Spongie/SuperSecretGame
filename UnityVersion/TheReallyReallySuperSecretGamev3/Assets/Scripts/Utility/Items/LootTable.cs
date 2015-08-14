using System.Collections.Generic;
using System.ComponentModel;

namespace Assets.Scripts.Utility
{
    public class LootTable : INotifyPropertyChanged
    {
        public LootTable()
        {
            ivItems = new List<LootTableItem>();
        }

        protected List<LootTableItem> ivItems;
        protected string ivName;

        public virtual List<LootTableItem> Items
        {
            get { return ivItems; }
            set 
            { 
                ivItems = value;
                FirePropertyChanged("Items");
            }
        }

        public string Name
        {
            get { return ivName; }
            set 
            { 
                ivName = value;
                FirePropertyChanged("Name");
            }
        }

        public List<string> GetRollingList()
        {
            var rollingList = new List<string>();

            foreach (LootTableItem item in Items)
            {
                for (int i = 0; i < item.DropChance; i++)
                {
                    rollingList.Add(item.ItemName);
                }
            }

            return rollingList;
        }

        protected void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
