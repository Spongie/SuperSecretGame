using System;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    class SkeletonArmor : Monster
    {
        public override void Start()
        {
            base.Start();
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

                Flip();

                AiList.RemoveAt(0);
            }

        }

        public override void UpdateIdle()
        {
            GoToState(UpdateGoForward);
        }

        public override void UpdateGoForward()
        {
            if (ivFacingRight)
            {
                ivRigidbody.velocity = GetRealSpeed();
            }
            else
            {
                ivRigidbody.velocity = ivRigidbody.velocity = new Vector2(-GetRealSpeed().x, GetRealSpeed().y);
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
                GoToState(UpdateAttack);
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

                GoToState(UpdateGoForward);

            }

        }

    }
}
