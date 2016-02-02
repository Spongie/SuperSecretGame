using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Assets.Scripts.Attacks;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Defense;
using Assets.Scripts.Utility;
using System.ComponentModel;

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
    public class Item : PropertyChanger
    {
        private string ivName;
        private float ivMagicDefense;
        private float ivResistance;
        private float ivDamage;
        private float ivMagicDamage;
        private float ivDefense;
        private float ivLuck;
        private ItemSlot ivSlot;
        private string ivIconName;
        
        private int ivMaxStackSize;
        private string ivID;
        private string ivDescription;

        [NonSerialized]
        private AttackEffectLoader ivEffectLoader;
        [NonSerialized]
        private DefenseEffectLoader ivDefenseEffectLoader;
        private AttackEffect ivSelectedAttackEffect;
        private DefenseEffect ivSelectedDefenseEfffect;
        private List<AttackEffect> ivAttackEffects;
        private List<DefenseEffect> ivDefenseEffects;

        public Item() 
        {
            ivEffectLoader = new AttackEffectLoader();
            ivDefenseEffectLoader = new DefenseEffectLoader();
            StackSize = 1;
            AttackEffects = new List<AttackEffect>();
            DefenseEffects = new List<DefenseEffect>();
        }

        public Item(Item original) :base()
        {
            ivName = original.Name;
            ivDefense = original.Defense;
            ivMagicDefense = original.MagicDefense;
            ivResistance = original.Resistance;
            ivDamage = original.Damage;
            ivMagicDamage = original.MagicDamage;
            ivLuck = original.Luck;
            ivSlot = original.Slot;
            ivIconName = original.IconName;
            StackSize = original.StackSize;
            MaxStackSize = original.MaxStackSize;
            AttackEffects = original.AttackEffects;
            DefenseEffects = original.DefenseEffects;

            if (string.IsNullOrEmpty(original.ID))
                GenerateID();
            else
                ID = original.ID;
        }

        public AttackEffect SelectedAttackEfffect
        {
            get { return ivSelectedAttackEffect; }
            set
            {
                ivSelectedAttackEffect = value;
                FirePropertyChanged("SelectedAttackEfffect");
            }
        }

        public DefenseEffect SelectedDefenseEfffect
        {
            get { return ivSelectedDefenseEfffect; }
            set
            {
                ivSelectedDefenseEfffect = value;
                FirePropertyChanged("SelectedDefenseEfffect");
            }
        }

        public string ID
        {
            get { return ivID; }
            set
            {
                ivID = value;
                FirePropertyChanged("ID");
            }
        }

        public short StackSize { get; set; }

        public int MaxStackSize
        {
            get { return ivMaxStackSize; }
            set
            {
                ivMaxStackSize = value;
                FirePropertyChanged("MaxStackSize");
            }
        }

        private float ivManaReg;
        private float ivHealthReg;
        private int ivMaxHealth;
        private int ivMaxMana;

        public float ManaPerSecond
        {
            get { return ivManaReg; }
            set
            {
                FirePropertyChanged("ManaPerSecond");
                ivManaReg = value;
            }
        }
        
        public float HealthPerSecond
        {
            get { return ivHealthReg; }
            set
            {
                FirePropertyChanged("HealthPerSecond");
                ivHealthReg = value;
            }
        }

        public int MaximumHealth
        {
            get { return ivMaxHealth; }
            set
            {
                ivMaxHealth = value;
                FirePropertyChanged("MaximumHealth");
            }
        }

        public int MaximumMana
        {
            get { return ivMaxMana; }
            set
            {
                ivMaxMana = value;
                FirePropertyChanged("MaximumMana");
            }
        }
       
        public List<AttackEffect> AttackEffects
        {
            get { return ivAttackEffects; }
            set
            {
                ivAttackEffects = value;
                FirePropertyChanged("AttackEffects");
            }
        }

        public List<DefenseEffect> DefenseEffects
        {
            get { return ivDefenseEffects; }
            set
            {
                ivDefenseEffects = value;
                FirePropertyChanged("DefenseEffects");
            }
        }

        public string IconName
        {
            get { return ivIconName; }
            set 
            { 
                ivIconName = value;
                FirePropertyChanged("IconName");
            }
        }

        public ItemSlot Slot
        {
            get { return ivSlot; }
            set 
            { 
                ivSlot = value;
                FirePropertyChanged("Slot");
            }
        }

        [JsonIgnore]
        public IEnumerable<ItemSlot> ItemSlots
        {
            get
            {
                return Enum.GetValues(typeof(ItemSlot)).Cast<ItemSlot>();
            }
        }

        [JsonIgnore]
        public List<string> AttackModifiers
        {
            get
            {
                if (ivEffectLoader == null)
                    ivEffectLoader = new AttackEffectLoader();

                return ivEffectLoader.GetAttackMethods();
            }
        }

        [JsonIgnore]
        public List<string> DefenseModifiers
        {
            get
            {
                if (ivDefenseEffectLoader == null)
                    ivDefenseEffectLoader = new DefenseEffectLoader();

                return ivDefenseEffectLoader.GetDefenseMethods();
            }
        }

        public string Name
        {
            get { return ivName; }
            set 
            { 
                ivName = value;
                FirePropertyChanged("Name");
            }
        }

        public float Damage
        {
            get { return ivDamage; }
            set 
            { 
                ivDamage = value;
                FirePropertyChanged("Damage");
            }
        }

        public float Defense
        {
            get { return ivDefense; }
            set 
            { 
                ivDefense = value;
                FirePropertyChanged("Defense");
            }
        }

        public float MagicDamage
        {
            get { return ivMagicDamage; }
            set 
            { 
                ivMagicDamage = value;
                FirePropertyChanged("MagicDamage");
            }
        }

        public float MagicDefense
        {
            get { return ivMagicDefense; }
            set 
            { 
                ivMagicDefense = value;
                FirePropertyChanged("MagicDefense");
            }
        }

        public float Luck
        {
            get { return ivLuck; }
            set 
            { 
                ivLuck = value;
                FirePropertyChanged("Luck");
            }
        }

        public float Resistance
        {
            get { return ivResistance; }
            set
            {
                ivResistance = value; 
                FirePropertyChanged("Resistance");
            }
        }

        public string Description
        {
            get
            {
                return ivDescription;
            }

            set
            {
                ivDescription = value;
                FirePropertyChanged("Description");
            }
        }

        public CStats GetStats()
        {
            var stats = new CStats()
            {
                Damage = ivDamage,
                Defense = ivDefense,
                MagicDamage = ivMagicDamage,
                MagicDefense = ivMagicDefense,
                Luck = ivLuck,
                Resistance = ivResistance,
                ManaPerSecond = ivManaReg,
                HealthPerSecond = ivHealthReg,
                MaximumHealth = ivMaxHealth,
                MaximumMana = ivMaxMana
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
            id += new Random().Next(1, 999999);

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
