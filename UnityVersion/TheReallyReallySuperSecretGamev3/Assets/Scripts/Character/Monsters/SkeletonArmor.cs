using System;
using CVCommon;
using TheSuperTrueRealCV.Utilities;
using CVCommon.Utility;
using UnityEngine;

namespace TheSuperTrueRealCV.EnemyAI
{
    class SkeletonArmor : Monster
    {
        public override void Start()
        {
            base.Start();

            CurrentStats.MaximumHealth = 100;
            CurrentStats.MaximumMana = 0;
            CurrentStats.Damage = 10;
            CurrentStats.Defense = 2;
            CurrentStats.MagicDamage = 0;
            CurrentStats.MagicDefense = 3;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void TurnAroundCheck()
        {
            if (target.transform.position.x < transform.position.x && ivFacingRight)
            {
                AiList.Add(() => UpdateTurnAround());
            }
            else if (target.transform.position.x > transform.position.x && !ivFacingRight)
            {
                AiList.Add(() => UpdateTurnAround());
            }
        }

        public override void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.5f);
                Newtimer = true;
            }
            if (AiTimer.Done)
            {
                Newtimer = false;
                if (ivFacingRight)
                {
                    ivFacingRight = false;
                }
                else
                {
                    ivFacingRight = true;
                }
                AiList.RemoveAt(0);
            }

        }

        public override void UpdateIdle()
        {
            AiList.Add(() => UpdateGoForward());
            AiList.RemoveAt(0);
        }

        public override void UpdateGoForward()
        {
            if (ivFacingRight)
            {
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);
            }
            else if (!ivFacingRight)
            {
                ivRigidbody.velocity = new Vector2(-3, ivRigidbody.velocity.y);
            }

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(1000);
                Newtimer = true;
            }

            if (Math.Abs(transform.position.x - target.transform.position.x) <= 150)
            {
                AiTimer.Restart(0);
                Newtimer = false;
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
            }


        }

        public override void UpdateAttack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.6f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                //ObjectManager.RegisterAttack(AttackCreator.CreateTestAttack(WorldPosition + new Vector2(100, 0), new Vector2(100, 100), this),this);

                AiTimer.Restart(1);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                HaveAttacked = false;

                TurnAroundCheck();

                AiList.Add(() => UpdateGoForward());
                AiList.RemoveAt(0);
            }

        }

    }
}
