﻿using Assets.Scripts.Character.Stat;
using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts.Buffs
{
    [Serializable]
    public class PoisonDebuff : Buff
    {
        public float DamagePerTick;
        public float AmountOfTicks;
        protected ManualTimer ivTickTimer;

        public PoisonDebuff() :this(0,0,0)
        {

        }

        public PoisonDebuff(float piDamagePerTick, float piAmountOfTicks, float piDuration) : base(new CStats(), piDuration)
        {
            if(piAmountOfTicks > 0)
                ivTickTimer = new ManualTimer(piDuration / piAmountOfTicks);

            DamagePerTick = piDamagePerTick;
            AmountOfTicks = piAmountOfTicks;
        }

        public bool ShouldTick { get; set; }

        public override void Update(float piDeltaTime)
        {
            if(ivTickTimer == null)
            {
                if(ivTimer != null)
                    ivTimer.Restart(Duration);

                ivTickTimer = new ManualTimer(Duration / AmountOfTicks);
            }

            base.Update(piDeltaTime);
            ivTickTimer.Update(piDeltaTime);

            if(ivTickTimer.Done)
                ShouldTick = true;
        }

        public virtual void Tick(CStats piTarget)
        {
            if (piTarget.Resist())
                return;

            piTarget.DealDamage(DamagePerTick);
            ShouldTick = false;
            ivTickTimer.Restart();
        }
    }
}
