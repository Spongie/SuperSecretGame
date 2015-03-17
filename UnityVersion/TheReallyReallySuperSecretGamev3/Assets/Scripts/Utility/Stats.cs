using UnityEngine;
namespace CVCommon.Utility
{
	public class Stats : MonoBehaviour
	{
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
