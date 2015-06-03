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

        public EditorLootTable()
        {
            LootItems = new ObservableCollection<LootTableItem>();
        }

        [JsonIgnore]
        public ObservableCollection<LootTableItem> LootItems { get; set; }

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
