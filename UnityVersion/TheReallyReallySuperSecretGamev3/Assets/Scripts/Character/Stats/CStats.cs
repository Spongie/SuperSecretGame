using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts.Character.Stat
{
    public delegate void DamageTakenDelegate(int amount);

    [Serializable]
    public class CStats
    {
        public event DamageTakenDelegate OnDamageTaken;

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
        }

        public int MaximumHealth;
        public int CurrentExp;
        public int MaximumExp;
        public int Level;

        public int CurrentHealth;
        public int MaximumMana;
        public int CurrentMana;
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

        public int ExpToLevel { get { return MaximumExp - CurrentExp; } }

        public float ManaPercentage
        {
            get { return (float)CurrentMana / (float)MaximumMana; }
        }

        public float HealthPercentage
        {
            get { return (float)CurrentHealth / (float)MaximumHealth; }
        }

        public float ExpPercentage
        {
            get { return (float)CurrentExp / (float)MaximumExp; }
        }

        public void DealDamage(float amount)
        {
            CurrentHealth -= (int)amount;

            if (CurrentHealth < 0)
                CurrentHealth = 0;

            if (OnDamageTaken != null)
                OnDamageTaken((int)amount);
        }

        public void DrainMana(int amount)
        {
            CurrentMana -= amount;
        }

        public void RewardExperience(int amount)
        {
            if (amount > ExpToLevel)
            {
                amount -= ExpToLevel;
                CurrentExp = 0;
                Level++;

                MaximumExp = (int)(1.33 * MaximumExp);

                RewardExperience(amount);
            }
            else
                CurrentExp += amount;
        }

        public bool Resist()
        {
            var roll = UnityEngine.Random.Range(0, 100);
            Logger.Log(string.Format("Rolled a {0} ", roll));
            return roll < (int)Resistance;
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
                Resistance = piFirst.Resistance + piOther.Resistance
            };

            newStats.CurrentHealth = (piFirst.CurrentHealth > piOther.CurrentHealth) ? piFirst.CurrentHealth : piOther.CurrentHealth;
            newStats.CurrentMana = (piFirst.CurrentMana > piOther.CurrentMana) ? piFirst.CurrentMana : piOther.CurrentMana;

            return newStats;
        }
    }
}
