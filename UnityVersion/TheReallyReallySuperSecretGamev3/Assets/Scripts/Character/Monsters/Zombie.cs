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

        public override void TurnAroundCheck()
        {
        }

        public override void UpdateTurnAround()
        {
        }

        public override void UpdateGoForward()
        {
            if (ivFacingRight && Math.Abs(transform.position.x - target.transform.position.x) <= 400)
            {
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);
            }
            else if (!ivFacingRight && Math.Abs(transform.position.x - target.transform.position.x) <= 400)
            {
                ivRigidbody.velocity = new Vector2(-3, ivRigidbody.velocity.y);
            }
        }

        public override void UpdateAttack()
        {
        }

        public override void UpdateIdle()
        {
            GoToState(UpdateGoForward);
        }
    }
}
