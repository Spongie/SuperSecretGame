using Assets.Scripts.Character.Stat;

namespace Assets.Scripts.Buffs
{
    public class StunBuff : Buff
    {
        public StunBuff(CStats piStats) : this(piStats, 10)
        {
        }

        public StunBuff(CStats piStats, float piDuration) : base(piStats, piDuration)
        {
        }
    }
}
