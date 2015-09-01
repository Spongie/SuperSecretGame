using Assets.Scripts.Character.Stat;

namespace Assets.Scripts.Buffs
{
    public class ChilledBuff : Buff
    {
        public ChilledBuff(CStats piStats) : this(piStats, 10)
        {
        }

        public ChilledBuff(CStats piStats, float piDuration) : base(piStats, piDuration)
        {
        }
    }
}
