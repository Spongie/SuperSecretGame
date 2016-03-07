using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;

namespace Assets.Scripts.Items
{
    public class ItemDropper : MonoBehaviour
    {
        [Range(1, 50)]
        public int MaxItemDrops;

        [Range(0, 100)]
        public int ChanceForDrop;

        private ItemManager ivItemManager;
        public LootTable LootTable;

        void Start()
        {
            ivItemManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemManager>();
        }

        public void DropItems()
        {
            float totalChance = ChanceForDrop + (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetTrueStats().Luck / 10f);
            float roll = Random.Range(0, 100);

            if (roll > totalChance)
                return;

            int nrOfDrops = Random.Range(1, MaxItemDrops);

            for (int i = 1; i <= nrOfDrops; i++)
            {
                Item item = LootTable.GetRollingList()[Random.Range(0, 1000)];

                var gameObject = new GameObject("DroppedItem" + i);
                DroppedItem drop = gameObject.AddComponent<DroppedItem>();
                drop.ItemDropped = item;
            }
        }
    }
}