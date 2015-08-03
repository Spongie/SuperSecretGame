using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buffs
{
    public class BuffContainer : MonoBehaviour
    {
        public CStats ivStats;
        public int NrOfBuffs;

        void Start()
        {
            Buffs = new List<Buff>();
        }

        public List<Buff> Buffs { get; private set; }

        void Update()
        {
            var expiredDebuffs = new List<Buff>();
            NrOfBuffs = Buffs.Count;

            foreach (var buff in Buffs)
            {
                buff.Update(Time.deltaTime);

                if (buff is PoisonDebuff)
                {
                    var debuff = (PoisonDebuff)buff;
                    if (debuff.ShouldTick)
                        debuff.Tick(ivStats);
                }

                if (buff.Expired)
                    expiredDebuffs.Add(buff);
            }

            foreach (var buff in expiredDebuffs)
            {
                Buffs.Remove(buff);
            }
        }

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
    }
}
