using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

        private void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
