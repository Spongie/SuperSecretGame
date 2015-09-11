using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    class Skeletonmusician : Monster
    {
        bool HaveUsedSuperAttack = false;

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

        public override void UpdateIdle()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            //är idle så den gör inget
            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(400, 800) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0, 7);

                if (newState == 0)
                {
                    GoToState(UpdateMagicAttack1);
                }
                else if (newState == 1)
                {
                    GoToState(UpdateAttack);
                }
                else if (newState == 2)
                {
                    GoToState(UpdateAttack2);
                }
                else if (newState == 3)
                {
                    GoToState(UpdateMoveBack);
                }
                else if (newState == 4)
                {
                    GoToState(UpdateGoForward);
                }
                else if (newState == 5)
                {
                    GoToState(UpdateJumpBack);
                }
                else if (newState == 6)
                {
                    GoToState(UpdateJumpForward);
                }
            }
        }

        public void UpdateDeepGrave()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                //fråga philip hur man gör !
                //bossen säger något coolt
                AiTimer.Restart(1);
                Newtimer = true;
            }

            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveUsedSuperAttack = true;
                HaveAttacked = true;
                //efter 6000 så har han laddat färdigt sin magic och själva hitboxen skapas
                AiTimer.Restart(8);
                //SKAPA SJÄLVA ANFALLET
            }

            if (AiTimer.Done)
            {
                TurnAroundCheck();
                HaveAttacked = false;
                Newtimer = false;
                GoToState(UpdateExhausted);
            }
        }

        public void UpdateMagicAttack1()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                //skickar sin fireball efter 1500
                AiTimer.Restart(1.7f);
                //skapa fireball anfallet
                HaveAttacked = true;
            }
            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                GoToState(UpdateIdle);
            }

        }

        public override void UpdateAttack()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.4f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                AiTimer.Restart(0.6f);
                //anfallet tar 600 att göra(kastar random instrument i en kast bana
                HaveAttacked = true;
            }
            if (AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                GoToState(UpdateIdle);
            }

        }

        public void UpdateAttack2()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(0.2f);
                Newtimer = true;
            }
            if (AiTimer.Done && HaveAttacked == false)
            {
                HaveAttacked = true;
                AiTimer.Restart(0.5f);
                //gör en stabb med sitt vapen och skickar ner en blixt framför sig
                //skapa anfallet
            }
            if(AiTimer.Done)
            {
                TurnAroundCheck();
                Newtimer = false;
                HaveAttacked = false;
                GoToState(UpdateIdle);
            }
        }

        public void UpdateMoveBack()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(700, 1000) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }

            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(-30, ivRigidbody.velocity.y);
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(30, ivRigidbody.velocity.y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0,6);

                if (newState == 0)
                {
                    GoToState(UpdateIdle);
                }
                else if (newState == 1)
                {
                    GoToState(UpdateJumpBack);
                }
                else if (newState == 2)
                {
                    GoToState(UpdateJumpForward);
                }
                else if (newState == 3)
                {
                    GoToState(UpdateMagicAttack1);
                }
                else if (newState == 4)
                {
                    GoToState(UpdateAttack);
                }
                else if (newState == 5)
                {
                    GoToState(UpdateAttack2);
                }
            }
        }

        public override void UpdateGoForward()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(300, 600) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }

            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(30, ivRigidbody.velocity.y);
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(-30, ivRigidbody.velocity.y);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0, 6);

                if (newState == 0)
                {
                    GoToState(UpdateIdle);
                }
                else if (newState == 1)
                {
                    GoToState(UpdateJumpBack);
                }
                else if (newState == 2)
                {
                    GoToState(UpdateJumpForward);
                }
                else if (newState == 3)
                {
                    GoToState(UpdateMagicAttack1);
                }
                else if (newState == 4)
                {
                    GoToState(UpdateAttack);
                }
                else if (newState == 5)
                {
                    GoToState(UpdateAttack2);
                }
            }
        }

        public void UpdateJumpBack()
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(700, 1000) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }
            //GLÖM INTE ATT GÖRA SÅ DEN HOPPAR ME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(-30, 20);
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(30, 20);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                GoToState(UpdateAttack);
            }
        }

        public void UpdateJumpForward() 
        {
            if (AiTimer.Done && HaveUsedSuperAttack == false && CurrentStats.stats.HealthPercentage <= 40)
            {
                Newtimer = false;
                GoToState(UpdateDeepGrave);
            }
            if (AiTimer.Done && Newtimer == false)
            {
                float timer = random.Next(700, 1000) / 1000;
                AiTimer.Restart(timer);
                Newtimer = true;
            }
            //GLÖM INTE ATT GÖRA SÅ DEN HOPPAR ME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (ivFacingRight == true)
            {
                ivRigidbody.velocity = new Vector2(3, 20);
            }
            else if (ivFacingRight == false)
            {
                ivRigidbody.velocity = new Vector2(-3, 20);
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                GoToState(UpdateAttack2);
            }
        }

        public void UpdateExhausted()
        {
            if (AiTimer.Done && Newtimer == false)
            {
                AiTimer.Restart(8);
                Newtimer = true;
            }

            if (AiTimer.Done)
            {
                Newtimer = false;
                TurnAroundCheck();
                newState = random.Next(0, 3);

                if (newState == 0)
                {
                    GoToState(UpdateAttack);
                }
                else if (newState == 1)
                {
                    GoToState(UpdateAttack2);
                }
                else if (newState == 2)
                {
                    GoToState(UpdateMagicAttack1);
                }
            }
        }
    }
}
