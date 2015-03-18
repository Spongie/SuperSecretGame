using CVCommon;
using TheSuperTrueRealCV.Utilities;
using CVCommon.Utility;
using UnityEngine;

namespace TheSuperTrueRealCV.EnemyAI
{
    class Skeleton : Monster
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
            Debug.Log("ATTACK");
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.2f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                //ny timer som avgör när han är färdig med sitt anfall och ska gå vidare med sin AI.
                AiTimer.Restart(0.5f);

                int speed = !ivFacingRight ? -200 : 200;
                ivAnimator.SetTrigger("Throw");
                //ObjectManager.RegisterAttack(AttackCreator.CreateSkeletonAttack(WorldPosition, this, new Vector2(speed , -300)), this);
            }

            if (AiTimer.Done)
            {
                HaveAttacked = false;
                Newtimer = false;
                ivRigidbody.velocity = Vector2.zero;
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
                float timer = random.Next(600, 1200) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }
            //timer som säger hur länge den ska gå
            //avgör vilken riktning som den ska gå
            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);                
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(-3, ivRigidbody.velocity.y); 
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
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(3, ivRigidbody.velocity.y);
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
                if (ivFacingRight == true)
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
    }
}
