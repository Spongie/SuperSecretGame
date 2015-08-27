using Assets.Scripts.Character.Stat;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buffs
{
    public class BuffContainer : MonoBehaviour, IBuffContainer
    {
        public BuffContainerController BuffController;
             
        void Start()
        {
            BuffController = new BuffContainerController();
        }

        void Update()
        {
            var expiredDebuffs = new List<Buff>();
            BuffController.NrOfBuffs = BuffController.Buffs.Count;

            foreach (var buff in BuffController.Buffs)
            {
                buff.Update(Time.deltaTime);

                if (buff is PoisonDebuff)
                {
                    var debuff = (PoisonDebuff)buff;
                    if (debuff.ShouldTick)
                        debuff.Tick(BuffController.Stats);
                }

                if (buff.Expired)
                    expiredDebuffs.Add(buff);
            }

            foreach (var buff in expiredDebuffs)
            {
                BuffController.Buffs.Remove(buff);
            }
        }

        public void ApplyBuff(Buff piBuff)
        {
            BuffController.ApplyBuff(piBuff);
        }

        public void ClearBuff(Buff piBuff)
        {
            BuffController.ClearBuff(piBuff);
        }

        public CStats GetBuffStats()
        {
            return BuffController.GetBuffStats();
        }

        public void SetStats(CStats piStats)
        {
            BuffController.SetStats(piStats);
        }
    }
}
