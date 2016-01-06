using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts.Character.Stats
{
    public delegate void DamageTakenDelegate(int amount);

    [Serializable]
    public class CStats
    {
        public event DamageTakenDelegate OnDamageTaken;

        [UnityEngine.SerializeField]
        public ResourceController Resources;

        public CStats() : this(0) { }

        public CStats(int piValue)
        {
            Damage = piValue;
            Defense = piValue;
            MagicDamage = piValue;
            MagicDefense = piValue;
            Luck = piValue;
            MaximumHealth = piValue;
            MaximumMana = piValue;
            Level = 1;
            CurrentExp = 0;
            MaximumExp = 100;

            HealthPerSecond = 0;
            ManaPerSecond = 0;
            Resources = new ResourceController(piValue, piValue);
        }

        public int MaximumHealth;
        public int CurrentExp;
        public int MaximumExp;
        public int Level;

        public int MaximumMana;
        public float Damage;
        public float Defense;
        public float MagicDamage;
        public float MagicDefense;
        public float Luck;
        public float Resistance;
        public float BaseCurseResist;
        public float BaseMpDrainResist;
        public float BaseLifeLeechResist;
        public float BaseInstaKillResist;
        public float BaseSlowResist;
        public float BaseStatsLossResist;
        public float BaseTimeStopResist;
        public float BaseFreezeResist;
        public float BaseFearResist;
        public float HealthPerSecond;
        public float ManaPerSecond;

        public int ExpToLevel { get { return MaximumExp - CurrentExp; } }

        public float ExpPercentage
        {
            get { return (float)CurrentExp / (float)MaximumExp; }
        }

        public void DealDamage(float amount)
        {
            Resources.DealDamage(amount);

            if (OnDamageTaken != null)
                OnDamageTaken((int)amount);
        }

        public void DrainMana(int amount)
        {
            Resources.DrainMana(amount);
        }

        public void RegenTick()
        {
            DealDamage((int)-HealthPerSecond);
            DrainMana((int)-ManaPerSecond);
        }

        private void LevelUp()
        {
            CurrentExp = 0;
            Level++;

            MaximumExp = (int)(1.33 * MaximumExp);

            Resources.BaseHealth += 10;
            Resources.BaseMana += 5;

            Resources.CurrentHealth += 10;
            Resources.BaseMana += 5;

            Damage++;
            Defense++;
            MagicDamage++;
            MagicDefense++;
            Luck++;
            Resistance += 0.05f;
        }

        public void RewardExperience(int amount)
        {
            if (amount > ExpToLevel)
            {
                amount -= ExpToLevel;

                LevelUp();
                RewardExperience(amount);
            }
            else
                CurrentExp += amount;
        }

        public bool Resist()
        {
            var roll = UnityEngine.Random.Range(0, 100);
            return roll < (int)Resistance;
        }

        public bool IsZero()
        {
            return MaximumHealth == 0 &&
                MaximumMana == 0 &&
                Damage == 0 &&
                Defense == 0 &&
                MagicDamage == 0 &&
                MagicDefense == 0 &&
                Luck == 0 &&
                Resistance == 0;
        }

        public static CStats operator +(CStats piFirst, CStats piOther)
        {
            var newStats = new CStats()
            {
                MaximumHealth = piFirst.MaximumHealth + piOther.MaximumHealth,
                MaximumMana = piFirst.MaximumMana + piOther.MaximumMana,
                Damage = piFirst.Damage + piOther.Damage,
                Defense = piFirst.Defense + piOther.Defense,
                MagicDamage = piFirst.MagicDamage + piOther.MagicDamage,
                MagicDefense = piFirst.MagicDefense + piOther.MagicDefense,
                Luck = piFirst.Luck + piOther.Luck,
                Resistance = piFirst.Resistance + piOther.Resistance,
                ManaPerSecond = piFirst.ManaPerSecond + piOther.ManaPerSecond,
                HealthPerSecond = piFirst.HealthPerSecond + piOther.HealthPerSecond,
                Resources = piFirst.Resources
            };

            return newStats;
        }
    }
}
