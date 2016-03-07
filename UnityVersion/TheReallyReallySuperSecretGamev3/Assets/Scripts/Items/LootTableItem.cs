using UnityEngine;

namespace Assets.Scripts.Items
{
    [System.Serializable]
    public class LootTableItem
    {
        public Item item;

        [Range(0f, 100f)]
        public float DropChance;     
        
        public string Name { get { return item.Name; } }  
    }
}
