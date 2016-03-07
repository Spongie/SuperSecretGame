using UnityEngine;

namespace Assets.Scripts.Items
{
    public class LootTableItem
    {
        public Item item;

        [Range(0f, 100f)]
        public float DropChance;     
        
        public string Name { get { return item.Name; } }  
    }
}
