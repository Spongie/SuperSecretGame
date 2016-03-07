using System;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    public class Wolf : Monster
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void TurnAroundCheck()
        {
            if (target.transform.position.x < transform.position.x && ivFacingRight)
            {
                GoToState(UpdateTurnAround, false);
            }
            else if (target.transform.position.x > transform.position.x && !ivFacingRight)
            {
                GoToState(UpdateTurnAround, false);
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

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
            }

            if (RangeFromPlayerX() <= 1)
            {
                AiTimer.Restart(0);
                Newtimer = false;
                GoToState(UpdateAttack);
            }
        }
        public override void UpdateIdle()
        {
            if (RangeFromPlayer() <= 5)
                GoToState(UpdateGoForward);
        }

        public override void UpdateAttack()
        {

            //FRÅGA PHILIP OCH GÖR RÄTT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.6f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
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
