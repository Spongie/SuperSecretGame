using Assets.Scripts.Character.Stats;
using System;

namespace Assets.Scripts.Buffs
{
    [Serializable]
    public class ManaDrainBuff : PoisonDebuff
    {
        public ManaDrainBuff(float piDamagePerTick, int piAmountOfTicks, float piDuration) : base(piDamagePerTick, piAmountOfTicks, piDuration)
        {
        }

        public override void Tick(CStats piTarget)
        {
            if (piTarget.Resist())
                return;

            piTarget.DrainMana((int)DamagePerTick);
            ShouldTick = false;
            ivTickTimer.Restart();
        }
    }
}