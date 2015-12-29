using System;

namespace Assets.Scripts.Character.Stats
{
    [Serializable]
    public class ResourceController
    {
        public int BaseHealth;
        public int ExtraHealth;
        public int BaseMana;
        public int ExtraMana;

        public int CurrentHealth;
        public int CurrentMana;

        public ResourceController(int baseHealth, int baseMana)
        {
            BaseHealth = baseHealth;
            BaseMana = baseMana;
            CurrentHealth = BaseHealth;
            CurrentMana = BaseMana;
        }

        public void Update(int newExtraHealth, int newExtraMana)
        {
            var oldHpPercent = HealthPercentage;
            var oldManaPercent = ManaPercentage;

            ExtraHealth = newExtraHealth;
            ExtraMana = newExtraMana;

            if (oldHpPercent != HealthPercentage)
                CurrentHealth = (int)(MaximumHealth * oldHpPercent);
            if (oldManaPercent != ManaPercentage)
                CurrentMana = (int)(MaximumMana * oldManaPercent);

            if (CurrentHealth > MaximumHealth)
                CurrentHealth = MaximumHealth;

            if (CurrentMana > MaximumMana)
                CurrentMana = MaximumMana;
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public int MaximumHealth
        {
            get { return BaseHealth + ExtraHealth; }
        }

        public int MaximumMana
        {
            get { return BaseMana + ExtraMana; }
        }

        public float ManaPercentage
        {
            get { return (float)CurrentMana / (float)MaximumMana; }
        }

        public float HealthPercentage
        {
            get { return (float)CurrentHealth / (float)MaximumHealth; }
        }

        internal void DealDamage(float amount)
        {
            CurrentHealth -= (int)amount;

            if (CurrentHealth < 0)
                CurrentHealth = 0;
            else if (CurrentHealth > MaximumHealth)
                CurrentHealth = MaximumHealth;
        }

        internal void DrainMana(int amount)
        {
            CurrentMana -= amount;

            if (CurrentMana < 0)
                CurrentMana = 0;
            else if (CurrentMana > MaximumMana)
                CurrentMana = MaximumMana;
        }
    }
}
