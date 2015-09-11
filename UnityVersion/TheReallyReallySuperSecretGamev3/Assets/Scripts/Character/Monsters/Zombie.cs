using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    class Zombie : Monster
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

        public override void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(1);
                Newtimer = true;
            }
            if (AiTimer.Done)
            {
                Newtimer = false;

                Flip();

                AiList.RemoveAt(0);
            }

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

            if (RangeFromPlayerX() > 10)
                GoToState(UpdateIdle);

        }

        public override void UpdateAttack()
        {
        }

        public override void UpdateIdle()
        {
            if(RangeFromPlayerX() <10)
            {
                TurnAroundCheck();
                GoToState(UpdateGoForward);
            }
        }
    }
}
