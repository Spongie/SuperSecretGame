using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Assets.Scripts.Attacks;

namespace Assets.Scripts.Items
{
    public enum ItemSlot
    {
        Weapon,
        Chest,
        Head,
        Legs,
        Feet,
        Hands,
        Neck,
        Ring,
        Shoulders,
        MinorGem1,
        MinorGem2,
        MinorGem3,
        MajorGem
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
        private string ivEffectName;
        private float ivEffectValue;
        private float ivEffectDuration;
        private int ivTicks;
        private int ivMaxStackSize;


        [NonSerialized]
        private AttackEffectLoader ivEffectLoader;

        public Item() 
        {
            ivEffectLoader = new AttackEffectLoader();
            EffectName = "None";
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
            StackSize = 1;

            if (string.IsNullOrEmpty(original.ID))
                GenerateID();
            else
                ID = original.ID;
        }

        private string ivID;

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public void GenerateID()
        {
            var id = DateTime.Now.Ticks.ToString();
            id += new Random().Next(1, 999999);

            ID = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
