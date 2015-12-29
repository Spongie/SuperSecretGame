using System.Collections.Generic;
using Assets.Scripts.Character.Stats;
using System;
using System.Linq;

namespace Assets.Scripts.Buffs
{
    [Serializable]
    public class BuffContainerController : IBuffContainer
    {
        public int NrOfBuffs;

        public BuffContainerController()
        {
            Buffs = new List<Buff>();
        }

        public CStats Stats { get; private set; }
        public List<Buff> Buffs { get; private set; }

        public void ApplyBuff(Buff piBuff)
        {
            Buffs.Add(piBuff);
        }

        public void ClearBuff(Buff piBuff)
        {
            Buffs.Remove(piBuff);
        }

        public CStats GetBuffStats()
        {
            var stats = new CStats();

            foreach (var buff in Buffs)
            {
                stats = stats + buff.StatChanges;
            }

            return stats;
        }

        public void SetStats(CStats piStats)
        {
            Stats = piStats;
        }

        public bool IsStunned()
        {
            return Buffs.Any(buff => buff.GetType() == typeof(StunBuff));
        }

        public bool IsChilled()
        {
            return Buffs.Any(buff => buff.GetType() == typeof(ChilledBuff));
        }

        public bool IsFeared()
        {
            return Buffs.Any(buff => buff.GetType() == typeof(FearBuff));
        }
    }
}
