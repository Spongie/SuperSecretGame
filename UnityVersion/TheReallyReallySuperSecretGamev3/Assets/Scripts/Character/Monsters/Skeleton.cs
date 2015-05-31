using CVCommon;
using TheSuperTrueRealCV.Utilities;
using CVCommon.Utility;
using UnityEngine;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Skeleton : Monster
    {
        public GameObject ThrowAttack;
        public Transform throwStartPosition;

        public override void Start()
        {
            base.Start();

            CurrentStats.stats.MaximumHealth = 100;
            CurrentStats.stats.MaximumMana = 0;
            CurrentStats.stats.Damage = 10;
            CurrentStats.stats.Defense = 2;
            CurrentStats.stats.MagicDamage = 0;
            CurrentStats.stats.MagicDefense = 3;
            ivFacingRight = true;
            AiTimer.Restart(0);
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
                    AiList.Add(() => UpdateGoBack());
                }

                else if (newState == 3 || newState == 4)
                {
                    AiList.Add(() => UpdateGoForward());
                }

                else if (newState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }

                else if (newState == 5)
                {
                    AiList.Add(() => UpdateAttack());
                }
                AiList.RemoveAt(0);
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

                int speed = !ivFacingRight ? -200 : 200;
                ivAnimator.SetTrigger("Throw");
                waitingForAnimation = true;
                //ObjectManager.RegisterAttack(AttackCreator.CreateSkeletonAttack(WorldPosition, this, new Vector2(speed , -300)), this);
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
                    AiList.Add(() => UpdateGoBack());
                }
                else if (newState == 3 || newState == 4)
                {
                    AiList.Add(() => UpdateGoForward());
                }
                else if (newState == 0)
                {
                    AiList.Add(() => UpdateIdle());
                }
                AiList.RemoveAt(0);
            }
        }

        public override void UpdateGoForward()
        {
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
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(-3, ivRigidbody.velocity.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            //ska röra sig i 1/4 ivRigidbody.velocity av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }
            
        }

        public void UpdateGoBack()
        {
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
                ivRigidbody.velocity = new Vector2(-3, ivRigidbody.velocity.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));

            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);
                ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));
            }
            //ska röra sig i 1/4 ivRigidbody.velocity av spelaren

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                AiList.Add(() => UpdateAttack());
                AiList.RemoveAt(0);
            }

        }

        public override void UpdateTurnAround()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(4);
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
                AiList.Add(() => UpdateTurnAround());
            }
            else if (target.transform.position.x > transform.position.x && ivFacingRight == false)
            {
                AiList.Add(() => UpdateTurnAround());
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

        private void Flip()
        {
            ivFacingRight = !ivFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
