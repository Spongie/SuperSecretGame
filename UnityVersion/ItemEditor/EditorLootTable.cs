using Assets.Scripts.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEditor
{
    public class EditorLootTable : LootTable
    {
        public EditorLootTable() : base()
        {
            LootItems = new ObservableCollection<LootTableItem>();
        }

        public EditorLootTable(LootTable piOriginal) : this()
        {
            LootItems = new ObservableCollection<LootTableItem>(piOriginal.Items);
            Name = piOriginal.Name;

            foreach (var item in LootItems)
            {
                item.DropChanceChanged += item_DropChanceChanged;
            }
        }

        [JsonIgnore]
        public ObservableCollection<LootTableItem> LootItems { get; set; }

        [JsonIgnore]
        public string TotalDropchance 
        {
            get { return TotalDropchanceOfTable(); }
        }

        public int SumDropChance()
        {
            return LootItems.Sum(item => item.DropChance);
        }

        private string TotalDropchanceOfTable()
        {
            return string.Format("{0} % ", LootItems.Sum(item => item.DropChance));
        }

        public override List<LootTableItem> Items
        {
            get
            {
                if(ivItems.Any())
                    return base.Items;
               
                return new List<LootTableItem>(LootItems);
            }
            set
            {
                base.Items = value;
            }
        }

        public void AddItem(LootTableItem piItem)
        {
            piItem.DropChanceChanged += item_DropChanceChanged;

            LootItems.Add(piItem);
        }

        void item_DropChanceChanged(object sender, EventArgs e)
        {
            FirePropertyChanged("TotalDropchance");
        }
    }
}
