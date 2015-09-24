using Assets.Scripts.Character.Stat;
using Assets.Scripts.Items;
using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public class PlayerSaveData
    {
        public PlayerSaveData(CStats piBaseStats, Inventory piInventory, List<string> piUnlockedSpells)
        {
            PlayerStats = piBaseStats;
            piInventory = PlayerInventory;
            UnlockedSpells = piUnlockedSpells;
        }

        public CStats PlayerStats { get; set; }
        public Inventory PlayerInventory { get; set; }
        public List<string> UnlockedSpells { get; set; }

    }
}
