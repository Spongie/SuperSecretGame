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
        }

        [JsonIgnore]
        public ObservableCollection<LootTableItem> LootItems { get; set; }

        [JsonIgnore]
        public int TotalDropchance 
        {
            get { return TotalDropchanceOfTable(); }
        }

        private int TotalDropchanceOfTable()
        {
            return LootItems.Sum(item => item.DropChance);
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
    }
}
