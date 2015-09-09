using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    class Skeleton : Monster
    {
        public GameObject ThrowAttack;
        public Transform throwStartPosition;

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

        public override void UpdateIdle() 
        {
            //är idle så den gör inget
            if(AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(400, 1500) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0, 6);

                if (newState == 1 || newState == 2)
                {
                    GoToState(UpdateGoBack);
                }

                else if (newState == 3 || newState == 4)
                {
                    GoToState(UpdateGoForward);
                }

                else if (newState == 0)
                {
                    GoToState(UpdateIdle);
                }

                else if (newState == 5)
                {
                    GoToState(UpdateAttack);
                }
            }
        }

        public override void UpdateAttack()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.4f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                //ny timer som avgör när han är färdig med sitt anfall och ska gå vidare med sin AI.
                AiTimer.Restart(1f);

                ivAnimator.SetTrigger("Throw");
                waitingForAnimation = true;
            }

            if (AiTimer.Done)
            {
                HaveAttacked = false;
                Newtimer = false;
                ivRigidbody.velocity = Vector2.zero;
                ivAnimator.SetFloat("Speed", 0f);
                TurnAroundCheck();
                newState = random.Next(0, 5);

                if (newState == 1 || newState == 2)
                {
                    GoToState(UpdateGoBack);
                }
                else if (newState == 3 || newState == 4)
                {
                    GoToState(UpdateGoForward);
                }
                else if (newState == 0)
                {
                    GoToState(UpdateIdle);
                }
            }
        }

        public override void UpdateGoForward()
        {
            Vector2 realSpeed = GetRealSpeed();

            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(1000, 1600) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(realSpeed.x, realSpeed.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(-realSpeed.x, realSpeed.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            //ska röra sig i 1/4 ivRigidbody.velocity av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                GoToState(UpdateAttack);
            }

        }

        public void UpdateGoBack()
        {
            var realSpeed = GetRealSpeed();

            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(700, 1400) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(-realSpeed.x, realSpeed.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));

            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(realSpeed.x, realSpeed.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            //ska röra sig i 1/4 ivRigidbody.velocity av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                GoToState(UpdateAttack);
            }

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
            if (target.transform.position.x < transform.position.x && ivFacingRight == true)
            {
                GoToState(UpdateTurnAround, false);
            }
            else if (target.transform.position.x > transform.position.x && ivFacingRight == false)
            {
                GoToState(UpdateTurnAround, false);
            }
        }

        public void OnThrowDone()
        {
            waitingForAnimation = false;
            GameObject attack = (GameObject)Instantiate(ThrowAttack, throwStartPosition.position, Quaternion.identity);
            attack.GetComponent<Attack>().Owner = gameObject;
            float xForce = 300 * (ivFacingRight ? 1 : -1);
            attack.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, 200));
        }

    }
}
