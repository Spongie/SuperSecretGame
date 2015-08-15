using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.Items;

public class ItemDropper : MonoBehaviour
{
    public string LootTableName;

    [Range(1, 50)]
    public int MaxItemDrops;

    private ItemManager ivItemManager;
    private LootTable ivLootTable;

    void Start()
    {
        ivItemManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemManager>();
        ivLootTable = ivItemManager.GetLootTable(LootTableName);
    }

    public void DropItems()
    {
        int nrOfDrops = Random.Range(1, MaxItemDrops);

        for (int i = 1; i <= nrOfDrops; i++)
        {
            string itemName = ivLootTable.GetRollingList()[Random.Range(0, 99)];

            var gameObject = new GameObject("DroppedItem" + i);
            DroppedItem drop = gameObject.AddComponent<DroppedItem>();
            drop.ItemDropped = ivItemManager.GetItemForDrop(itemName);
        }
    }
}
