using System;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    class Bat : Monster
    {
        public override void Start()
        {
            base.Start();

            CurrentStats.stats.MaximumHealth = 100;
            CurrentStats.stats.MaximumMana = 0;
            CurrentStats.stats.Damage = 10;
            CurrentStats.stats.Defense = 2;
            CurrentStats.stats.MagicDamage = 0;
            CurrentStats.stats.MagicDefense = 3;

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void UpdateIdle()
        {
            ivRigidbody.gravityScale = 0f;
            if (Vector2.Distance(transform.position, target.transform.position) <= 200 && target.transform.position.x <= transform.position.x)
            {
                AiList.Add(() => UpdateDiveLeft());
                ivFacingRight = false;
                AiList.RemoveAt(0);
            }
            else if (Vector2.Distance(transform.position, target.transform.position) <= 200 && target.transform.position.x >= transform.position.x)
            {
                AiList.Add(() => UpdateDiveRight());
                ivFacingRight = true;
                AiList.RemoveAt(0);
            }
        }

        public void UpdateDiveLeft()
        {
            //FIXA SÅ DEN FALLER NERÅT
            ivRigidbody.gravityScale = 1f;

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(1.5f);
                Newtimer = true;
            }

            if (Math.Abs(transform.position.y - target.transform.position.y) <= 5 || AiTimer.Done)
            {
                AiTimer.Restart(0);
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateChillLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/NER
            ivRigidbody.velocity = new Vector2(-100, ivRigidbody.velocity.y);


            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.35f);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateRiseLeft()
        {
            //FIXA SPEED ÅT VÄNSTER/UP
            ivRigidbody.velocity = new Vector2(-30, -25);

            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.3f);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillLeft());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateDiveRight()
        {
            ivRigidbody.gravityScale = 1f;
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(1.5f);
                Newtimer = true;
            }

            if (Math.Abs(transform.position.y - target.transform.position.y) <= 5 || AiTimer.Done)
            {
                AiTimer.Restart(0);
                Newtimer = false;
                AiList.Add(() => UpdateRiseRight());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateChillRight()
        {
            //FIXA SPEED ÅT HÖGER/NER
            ivRigidbody.velocity = new Vector2(30, ivRigidbody.velocity.y);
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.35f);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateRiseRight());
                AiList.RemoveAt(0);
            }
        }

        public void UpdateRiseRight()
        {
            //FIXA SPEED ÅT HÖGER/UP
            ivRigidbody.velocity = new Vector2(30, -25);
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.3f);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                AiList.Add(() => UpdateChillRight());
                AiList.RemoveAt(0);
            }
        }

        public override void TurnAroundCheck()
        {
        }

        public override void UpdateTurnAround()
        {
        }

        public override void UpdateGoForward()
        {
        }

        public override void UpdateAttack()
        {
        }
    }
}
