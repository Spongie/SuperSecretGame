using UnityEngine;
using Assets.Scripts.Utility;
using System.Collections.Generic;

public class ItemDropper : MonoBehaviour
{
    public string LootTableName;

    [Range(1, 10)]
    public int MaxItemDrops;

    private ItemManager ivItemManager;
    private LootTable ivLootTable;

    void Start()
    {
        ivItemManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemManager>();
        ivLootTable = ivItemManager.GetLootTable(LootTableName);
    }

    public List<Item> GetDroppedItems()
    {
        var drops = new List<Item>();
        int nrOfDrops = Random.Range(0, MaxItemDrops);

        for (int i = 1; i <= nrOfDrops; i++)
        {
            string itemName = ivLootTable.GetRollingList()[Random.Range(0, 99)];
            drops.Add(ivItemManager.GetItemForDrop(itemName));
        }

        return drops;
    }
}
