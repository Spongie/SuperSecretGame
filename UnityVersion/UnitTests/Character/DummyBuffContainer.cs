using Assets.Scripts.Buffs;
using System;
using Assets.Scripts.Character.Stat;

namespace UnitTests.Character
{
    class DummyBuffContainer : IBuffContainer
    {
        public void ApplyBuff(Buff piBuff)
        {
            throw new NotImplementedException();
        }

        public void ClearBuff(Buff piBuff)
        {
            throw new NotImplementedException();
        }

        public CStats GetBuffStats()
        {
            var stats = new CStats();
            stats.Damage = 10;

            return stats;
        }

        public void SetStats(CStats piStats)
        {
        }
    }
}
