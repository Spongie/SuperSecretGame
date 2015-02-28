using CVCommon;

namespace TheSuperTrueRealCV.Utilities
{
    public static class DamageCalcualtor
    {
        public static float CalculateDamage(Stats piAttacker, Stats piTarget, AttackDamageScaling piAttackScaling)
        {
            float baseDamage = (piAttacker.Damage * piAttackScaling.Damage) + (piAttacker.MagicDamage * piAttackScaling.Magic);

            float physicalDefense = piAttackScaling.Damage > 0 ? piTarget.Defense : 0;

            float magicDefense = piAttackScaling.Magic > 0 ? piTarget.MagicDefense : 0;

            return baseDamage - physicalDefense - magicDefense;
        }
    }
}
