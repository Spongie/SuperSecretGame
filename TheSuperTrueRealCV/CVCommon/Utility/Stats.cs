﻿namespace CVCommon.Utility
{
    public class Stats
    {
        private int maxHealth;

        public int MaximumHealth 
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                if (CurrentHealth == 0)
                    CurrentHealth = maxHealth;
            }
        }

        public int CurrentExp { get; set; }
        public int MaximumExp { get; set; }
        public int ExpToLevel { get { return MaximumExp - CurrentExp; } }
        public int Level { get; set; }

        public int CurrentHealth { get; set; }
        public int MaximumMana { get; set; }
        public int CurrentMana { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public float MagicDamage { get; set; }
        public float MagicDefense { get; set; }
        public float Luck { get; set; }
        public float Resistance { get; set; }
        public float BaseCurseResist { get; set; }
        public float BaseMpDrainResist { get; set; }
        public float BaseLifeLeechResist { get; set; }
        public float BaseInstaKillResist { get; set; }
        public float BaseSlowResist { get; set; }
        public float BaseStatsLossResist { get; set; }
        public float BaseTimeStopResist { get; set; }
        public float BaseFreezeResist { get; set; }
        public float BaseFearResist { get; set; }


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
    }
}
