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
        /*
        public AttackEffect SelectedAttackEfffect
        {
            get { return SelectedAttackEffect; }
            set
            {
                SelectedAttackEffect = value;
                FirePropertyChanged("SelectedAttackEfffect");
            }
        }

        public DefenseEffect SelectedDefenseEfffect
        {
            get { return SelectedDefenseEfffect; }
            set
            {
                SelectedDefenseEfffect = value;
                FirePropertyChanged("SelectedDefenseEfffect");
            }
        }

        public string ID
        {
            get { return ID; }
            set
            {
                ID = value;
                FirePropertyChanged("ID");
            }
        }


        public int MaxStackSize
        {
            get { return MaxStackSize; }
            set
            {
                MaxStackSize = value;
                FirePropertyChanged("MaxStackSize");
            }
        }*/

        public short StackSize;

        public float ManaReg;
        public float HealthReg;
        public int MaxHealth;
        public int MaxMana;

        /*
        public float ManaPerSecond
        {
            get { return ManaReg; }
            set
            {
                FirePropertyChanged("ManaPerSecond");
                ManaReg = value;
            }
        }
        
        public float HealthPerSecond
        {
            get { return HealthReg; }
            set
            {
                FirePropertyChanged("HealthPerSecond");
                HealthReg = value;
            }
        }

        public int MaximumHealth
        {
            get { return MaxHealth; }
            set
            {
                MaxHealth = value;
                FirePropertyChanged("MaximumHealth");
            }
        }

        public int MaximumMana
        {
            get { return MaxMana; }
            set
            {
                MaxMana = value;
                FirePropertyChanged("MaximumMana");
            }
        }
       
        public List<AttackEffect> AttackEffects
        {
            get { return AttackEffects; }
            set
            {
                AttackEffects = value;
                FirePropertyChanged("AttackEffects");
            }
        }

        public List<DefenseEffect> DefenseEffects
        {
            get { return DefenseEffects; }
            set
            {
                DefenseEffects = value;
                FirePropertyChanged("DefenseEffects");
            }
        }

        public string IconName
        {
            get { return IconName; }
            set 
            { 
                IconName = value;
                FirePropertyChanged("IconName");
            }
        }

        public ItemSlot Slot
        {
            get { return Slot; }
            set 
            { 
                Slot = value;
                FirePropertyChanged("Slot");
            }
        }*/

        [JsonIgnore]
        public IEnumerable<ItemSlot> ItemSlots
        {
            get
            {
                return Enum.GetValues(typeof(ItemSlot)).Cast<ItemSlot>();
            }
        }
        /*
        [JsonIgnore]
        public List<string> AttackModifiers
        {
            get
            {
                if (EffectLoader == null)
                    EffectLoader = new AttackEffectLoader();

                return EffectLoader.GetAttackMethods();
            }
        }

        [JsonIgnore]
        public List<string> DefenseModifiers
        {
            get
            {
                if (DefenseEffectLoader == null)
                    DefenseEffectLoader = new DefenseEffectLoader();

                return DefenseEffectLoader.GetDefenseMethods();
            }
        }

        public string Name
        {
            get { return Name; }
            set 
            { 
                Name = value;
                FirePropertyChanged("Name");
            }
        }

        public float Damage
        {
            get { return Damage; }
            set 
            { 
                Damage = value;
                FirePropertyChanged("Damage");
            }
        }

        public float Defense
        {
            get { return Defense; }
            set 
            { 
                Defense = value;
                FirePropertyChanged("Defense");
            }
        }

        public float MagicDamage
        {
            get { return MagicDamage; }
            set 
            { 
                MagicDamage = value;
                FirePropertyChanged("MagicDamage");
            }
        }

        public float MagicDefense
        {
            get { return MagicDefense; }
            set 
            { 
                MagicDefense = value;
                FirePropertyChanged("MagicDefense");
            }
        }

        public float Luck
        {
            get { return Luck; }
            set 
            { 
                Luck = value;
                FirePropertyChanged("Luck");
            }
        }

        public float Resistance
        {
            get { return Resistance; }
            set
            {
                Resistance = value; 
                FirePropertyChanged("Resistance");
            }
        }

        public string Description
        {
            get
            {
                return Description;
            }

            set
            {
                Description = value;
                FirePropertyChanged("Description");
            }
        }*/

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
