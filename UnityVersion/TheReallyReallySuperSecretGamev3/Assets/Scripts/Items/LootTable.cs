using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(menuName = "LootTable")]
    public class LootTable : ScriptableObject
    {
        public string Name;
        public List<LootTableItem> Items;

        public List<Item> GetRollingList()
        {
            var rollingList = new List<Item>();

            foreach (LootTableItem item in Items)
            {
                for (int i = 0; i < item.DropChance * 10; i++)
                {
                    rollingList.Add(item.item);
                }
            }

            return rollingList;
        }
    }
}
