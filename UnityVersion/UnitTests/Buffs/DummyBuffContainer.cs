using Assets.Scripts.Buffs;
using System;
using Assets.Scripts.Character.Stat;

namespace UnitTests.Buffs
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

        public bool IsChilled()
        {
            throw new NotImplementedException();
        }

        public bool IsFeared()
        {
            throw new NotImplementedException();
        }

        public bool IsStunned()
        {
            throw new NotImplementedException();
        }

        public void SetStats(CStats piStats)
        {
        }
    }
}
