using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Assets.Scripts.Attacks;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Defense;
using UnityEngine;
using Assets.Scripts.Attacks.Modifier;

namespace Assets.Scripts.Items
{
    public enum ItemSlot
    {
        MajorGem,
        MinorGem,
        Neck,
        Ring,
        Consumable
    }

    [Serializable]
    [CreateAssetMenu(menuName = "Item")]
    public class Item : ScriptableObject
    {
        public string Name;
        public float MagicDefense;
        public float Resistance;
        public float Damage;
        public float MagicDamage;
        public float Defense;
        public float Luck;
        public ItemSlot Slot;
        public Sprite Icon;
        
        public int MaxStackSize;
        public string ID;
        public string Description;
        public short StackSize;
        public float ManaReg;
        public float HealthReg;
        public int MaxHealth;
        public int MaxMana;

        [NonSerialized]
        private AttackEffectLoader EffectLoader;
        [NonSerialized]
        public DefenseEffectLoader DefenseEffectLoader;
        public List<AttackModifier> AttackEffects;
        public List<DefenseEffect> DefenseEffects;

        public Item() 
        {
            EffectLoader = new AttackEffectLoader();
            DefenseEffectLoader = new DefenseEffectLoader();
            StackSize = 1;
            AttackEffects = new List<AttackModifier>();
            DefenseEffects = new List<DefenseEffect>();
        }

        public Item(Item original) :base()
        {
            Name = original.Name;
            Defense = original.Defense;
            MagicDefense = original.MagicDefense;
            Resistance = original.Resistance;
            Damage = original.Damage;
            MagicDamage = original.MagicDamage;
            Luck = original.Luck;
            Slot = original.Slot;
            Icon = original.Icon;
            StackSize = original.StackSize;
            MaxStackSize = original.MaxStackSize;
            AttackEffects = original.AttackEffects;
            DefenseEffects = original.DefenseEffects;

            if (string.IsNullOrEmpty(original.ID))
                GenerateID();
            else
                ID = original.ID;
        }

        [JsonIgnore]
        public IEnumerable<ItemSlot> ItemSlots
        {
            get
            {
                return Enum.GetValues(typeof(ItemSlot)).Cast<ItemSlot>();
            }
        }

        public CStats GetStats()
        {
            var stats = new CStats()
            {
                Damage = Damage,
                Defense = Defense,
                MagicDamage = MagicDamage,
                MagicDefense = MagicDefense,
                Luck = Luck,
                Resistance = Resistance,
                ManaPerSecond = ManaReg,
                HealthPerSecond = HealthReg,
                MaximumHealth = MaxHealth,
                MaximumMana = MaxMana
            };

            return stats;
        }

        public bool IsSingleSlotItem()
        {
            return Slot == ItemSlot.Neck || Slot == ItemSlot.MajorGem;
        }

        public void GenerateID()
        {
            var id = DateTime.Now.Ticks.ToString();
            id += new System.Random().Next(1, 999999);

            ID = id;
        }

        public string GetListString()
        {
            if (Name == "Empty")
                return Name;

            return string.Format("{0} - {1}/{2}", Name, StackSize, MaxStackSize);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
