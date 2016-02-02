using Assets.Scripts.Character.Stats;
using Assets.Scripts.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Attacks
{
    [Serializable]
    public class AttackEffect : PropertyChanger
    {
        [NonSerialized]
        private AttackEffectLoader ivEffectLoader;

        public AttackEffect()
        {
            Stats = new CStats();
            ivEffectLoader = new AttackEffectLoader();
        }

        public string name = "None";
        public float power = 0.0f;
        public float duration = 0.0f;
        public int ticks = 0;
        public CStats stats;

        [JsonIgnore]
        public List<string> AttackModifiers
        {
            get { return ivEffectLoader.GetAttackMethods(); }
        }

        public CStats Stats
        {
            get
            {
                return stats;
            }

            set
            {
                stats = value;
                FirePropertyChanged("Stats");
            }
        }

        public int Ticks
        {
            get
            {
                return ticks;
            }

            set
            {
                ticks = value;
                FirePropertyChanged("Ticks");
            }
        }

        public float Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
                FirePropertyChanged("Duration");
            }
        }

        public float Power
        {
            get
            {
                return power;
            }

            set
            {
                power = value;
                FirePropertyChanged("Power");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
