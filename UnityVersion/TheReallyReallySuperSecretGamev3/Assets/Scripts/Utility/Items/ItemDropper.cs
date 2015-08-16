﻿using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.Items;
using CVCommon.Utility;
using Assets.Scripts.Character;

public class ItemDropper : MonoBehaviour
{
    public string LootTableName;

    [Range(1, 50)]
    public int MaxItemDrops;

    [Range(0, 100)]
    public int ChanceForDrop;

    private ItemManager ivItemManager;
    private LootTable ivLootTable;

    void Start()
    {
        ivItemManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemManager>();
        ivLootTable = ivItemManager.GetLootTable(LootTableName);
    }

    public void DropItems()
    {
        float totalChance = ChanceForDrop + (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetTrueStats().Luck / 10f);
        float roll = Random.Range(0, 100);

        if (roll < totalChance)
            return;

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
