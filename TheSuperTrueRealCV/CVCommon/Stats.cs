namespace CVCommon
{
    public class Stats
    {
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumMana { get; set; }
        public int CurrentMana { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public float MagicDamage { get; set; }
        public float MagicDefense { get; set; }
        public float Luck { get; set; }
        public float Other { get; set; }

        public float MAnaPercentage
        {
            get { return CurrentMana / MaximumMana; }
        }

        public float HealthPercentage
        {
            get { return CurrentHealth / MaximumHealth; }
        }

        public void DealDamage(float amount)
        {
            CurrentHealth -= (int)amount;
        }

        public void DrainMana(int amount)
        {
            CurrentMana -= amount;
        }
    }
}
