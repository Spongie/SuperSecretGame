using Assets.Scripts.Character.Stats;

namespace Assets.Scripts.Buffs
{
    public class FearBuff : Buff
    {
        public FearBuff(CStats piStats) : this(piStats, 10)
        {
        }

        public FearBuff(CStats piStats, float piDuration) : base(piStats, piDuration)
        {
        }
    }
}
