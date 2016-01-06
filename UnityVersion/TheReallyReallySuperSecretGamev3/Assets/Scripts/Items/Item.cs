using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Assets.Scripts.Attacks;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Defense;

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
    public class Item : INotifyPropertyChanged
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
        
        private int ivTicks;
        private int ivMaxStackSize;
        private string ivID;
        private string ivDescription;

        private string ivEffectName;
        private float ivEffectValue;
        private float ivEffectDuration;
        private float ivEffectMagicDefense;
        private float ivEffectResistance;
        private float ivEffectDamage;
        private float ivEffectMagicDamage;
        private float ivEffectDefense;
        private float ivEffectLuck;

        private string ivDefenseEffectName;
        private float ivDefenseEffectValue;
        private float ivDefenseEffectDuration;
        private float ivDefenseEffectMagicDefense;
        private float ivDefenseEffectResistance;
        private float ivDefenseEffectDamage;
        private float ivDefenseEffectMagicDamage;
        private float ivDefenseEffectDefense;
        private float ivDefenseEffectLuck;

        [NonSerialized]
        private AttackEffectLoader ivEffectLoader;
        private DefenseEffectLoader ivDefenseEffectLoader;
        private int ivDefenseTicks;

        public Item() 
        {
            ivEffectLoader = new AttackEffectLoader();
            ivDefenseEffectLoader = new DefenseEffectLoader();
            EffectName = "None";
            StackSize = 1;
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
            EffectDuration = original.EffectDuration;
            EffectName = original.EffectName;
            EffectTicks = original.EffectTicks;
            EffectValue = original.EffectValue;
            StackSize = original.StackSize;
            MaxStackSize = original.MaxStackSize;

            if (string.IsNullOrEmpty(original.ID))
                GenerateID();
            else
                ID = original.ID;
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
        
        private float ivEffectManaReg;
        private float ivEffectHealthReg;
        private int ivEffectMaxHealth;
        private int ivEffectMaxMana;

        public float EffectManaPerSecond
        {
            get { return ivEffectManaReg; }
            set
            {
                FirePropertyChanged("EffectManaPerSecond");
                ivEffectManaReg = value;
            }
        }

        public float EffectHealthPerSecond
        {
            get { return ivEffectHealthReg; }
            set
            {
                FirePropertyChanged("EffectHealthPerSecond");
                ivEffectHealthReg = value;
            }
        }

        public int EffectMaximumHealth
        {
            get { return ivEffectMaxHealth; }
            set
            {
                ivEffectMaxHealth = value;
                FirePropertyChanged("EffectMaximumHealth");
            }
        }

        public int EffectMaximumMana
        {
            get { return ivEffectMaxMana; }
            set
            {
                ivEffectMaxMana = value;
                FirePropertyChanged("EffectMaximumMana");
            }
        }
        
        private float ivDefenseEffectManaReg;
        private float ivDefenseEffectHealthReg;
        private int ivDefenseEffectMaxHealth;
        private int ivDefenseEffectMaxMana;

        public float DefenseEffectManaPerSecond
        {
            get { return ivDefenseEffectManaReg; }
            set
            {
                FirePropertyChanged("DefenseEffectManaPerSecond");
                ivDefenseEffectManaReg = value;
            }
        }

        public float DefenseEffectHealthPerSecond
        {
            get { return ivDefenseEffectHealthReg; }
            set
            {
                FirePropertyChanged("DefenseEffectHealthPerSecond");
                ivDefenseEffectHealthReg = value;
            }
        }

        public int DefenseEffectMaximumHealth
        {
            get { return ivDefenseEffectMaxHealth; }
            set
            {
                ivDefenseEffectMaxHealth = value;
                FirePropertyChanged("DefenseEffectMaximumHealth");
            }
        }

        public int DefenseEffectMaximumMana
        {
            get { return ivDefenseEffectMaxMana; }
            set
            {
                ivDefenseEffectMaxMana = value;
                FirePropertyChanged("DefenseEffectMaximumMana");
            }
        }

        public string EffectName
        {
            get { return ivEffectName; }
            set 
            { 
                ivEffectName = value;
                FirePropertyChanged("EffectName");
            }
        }

        public float EffectValue
        {
            get { return ivEffectValue; }
            set 
            {
                ivEffectValue = value;
                FirePropertyChanged("EffectValue");
            }
        }

        public float EffectDuration
        {
            get { return ivEffectDuration; }
            set
            {
                ivEffectDuration = value;
                FirePropertyChanged("EffectDuration");
            }
        }

        public int EffectTicks
        {
            get { return ivTicks; }
            set
            {
                ivTicks = value;
                FirePropertyChanged("Ticks");
            }
        }

        public CStats EffectStats
        {
            get
            {
                var stats = new CStats()
                {
                    Damage = EffectDamage,
                    MagicDamage = EffectMagicDamage,
                    MagicDefense = EffectMagicDamage,
                    Defense = EffectDefense,
                    Luck = EffectLuck,
                    Resistance = EffectResistance,
                    MaximumMana = ivEffectMaxMana,
                    MaximumHealth = ivEffectMaxHealth,
                    ManaPerSecond = ivEffectManaReg,
                    HealthPerSecond = ivEffectHealthReg
                };

                return stats;
            }
        }

        public float EffectDamage
        {
            get { return ivEffectDamage; }
            set
            {
                ivEffectDamage = value;
                FirePropertyChanged("EffectDamage");
            }
        }

        public float EffectDefense
        {
            get { return ivEffectDefense; }
            set
            {
                ivEffectDefense = value;
                FirePropertyChanged("EffectDefense");
            }
        }

        public float EffectMagicDamage
        {
            get { return ivEffectMagicDamage; }
            set
            {
                ivEffectMagicDamage = value;
                FirePropertyChanged("EffectMagicDamage");
            }
        }

        public float EffectMagicDefense
        {
            get { return ivEffectMagicDefense; }
            set
            {
                ivEffectMagicDefense = value;
                FirePropertyChanged("EffectMagicDefense");
            }
        }

        public float EffectLuck
        {
            get { return ivEffectLuck; }
            set
            {
                ivEffectLuck = value;
                FirePropertyChanged("Luck");
            }
        }

        public float EffectResistance
        {
            get { return ivEffectResistance; }
            set
            {
                ivEffectResistance = value;
                FirePropertyChanged("Resistance");
            }
        }

        public string DefenseEffectName
        {
            get { return ivDefenseEffectName; }
            set
            {
                ivDefenseEffectName = value;
                FirePropertyChanged("DefenseEffectName");
            }
        }

        public float DefenseEffectValue
        {
            get { return ivDefenseEffectValue; }
            set
            {
                ivDefenseEffectValue = value;
                FirePropertyChanged("DefenseEffectValue");
            }
        }

        public float DefenseEffectDuration
        {
            get { return ivDefenseEffectDuration; }
            set
            {
                ivDefenseEffectDuration = value;
                FirePropertyChanged("DefenseEffectDuration");
            }
        }

        public int DefenseEffectTicks
        {
            get { return ivDefenseTicks; }
            set
            {
                ivDefenseTicks = value;
                FirePropertyChanged("DefenseTicks");
            }
        }

        public CStats DefenseEffectStats
        {
            get
            {
                var stats = new CStats()
                {
                    Damage = DefenseEffectDamage,
                    MagicDamage = DefenseEffectMagicDamage,
                    MagicDefense = DefenseEffectMagicDamage,
                    Defense = DefenseEffectDefense,
                    Luck = DefenseEffectLuck,
                    Resistance = DefenseEffectResistance,
                    MaximumMana = ivDefenseEffectMaxMana,
                    MaximumHealth = ivDefenseEffectMaxHealth,
                    ManaPerSecond = ivDefenseEffectManaReg,
                    HealthPerSecond = ivDefenseEffectHealthReg
                };

                return stats;
            }
        }

        public float DefenseEffectDamage
        {
            get { return ivDefenseEffectDamage; }
            set
            {
                ivDefenseEffectDamage = value;
                FirePropertyChanged("DefenseEffectDamage");
            }
        }

        public float DefenseEffectDefense
        {
            get { return ivDefenseEffectDefense; }
            set
            {
                ivDefenseEffectDefense = value;
                FirePropertyChanged("DefenseEffectDefense");
            }
        }

        public float DefenseEffectMagicDamage
        {
            get { return ivDefenseEffectMagicDamage; }
            set
            {
                ivDefenseEffectMagicDamage = value;
                FirePropertyChanged("DefenseEffectMagicDamage");
            }
        }

        public float DefenseEffectMagicDefense
        {
            get { return ivDefenseEffectMagicDefense; }
            set
            {
                ivDefenseEffectMagicDefense = value;
                FirePropertyChanged("DefenseEffectMagicDefense");
            }
        }

        public float DefenseEffectLuck
        {
            get { return ivDefenseEffectLuck; }
            set
            {
                ivDefenseEffectLuck = value;
                FirePropertyChanged("DefenseLuck");
            }
        }

        public float DefenseEffectResistance
        {
            get { return ivDefenseEffectResistance; }
            set
            {
                ivDefenseEffectResistance = value;
                FirePropertyChanged("DefenseResistance");
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
            get { return ivEffectLoader.GetAttackMethods(); }
        }

        [JsonIgnore]
        public List<string> DefenseModifiers
        {
            get { return ivDefenseEffectLoader.GetDefenseMethods(); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
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
