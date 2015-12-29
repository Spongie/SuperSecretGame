using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character.Monsters
{
    [RequireComponent(typeof(BuffContainer))]
    [RequireComponent(typeof(Timer))]
    [RequireComponent(typeof(ItemDropper))]
    public abstract class Monster : Character_Controller
    {
        protected GameObject target;
        protected List<Action> AiList;
        protected System.Random random;
        public Timer AiTimer;
        protected bool HaveAttacked;
        protected bool Newtimer;
        protected int newState;
        protected bool waitingForAnimation = false;
        public MonsterTypes MonsterType = MonsterTypes.Humanoid;
        protected BuffContainer Buffs;
        protected bool stunnedLastFrame = false;
        protected Attack disabledAttackOnStun;

        public override void Start()
        {
            base.Start();

            AiList = new List<Action>();
            random = new System.Random();
            HaveAttacked = false;
            Newtimer = false;
            AiTimer = GetComponent<Timer>();
            AiTimer.Restart(0);
            ExpReward = 25;
            Activate();

            Buffs = GetComponent<BuffContainer>();
            Buffs.SetStats(CurrentStats.stats);
            ivFacingRight = true;
        }

        public void Deactivate()
        {
            AiList.Clear();
            target = null;
        }

        public void Activate()
        {
            this.target = GameObject.FindGameObjectWithTag("Player");

            if (target.transform.position.x <= transform.position.x)
                ivFacingRight = false;
            else
                ivFacingRight = true;
            
            AiList.Add(() => UpdateIdle());
        }

        public CStats GetTrueStats()
        {
            return CurrentStats.stats + Buffs.GetBuffStats();
        }

        protected Vector2 GetRealSpeed()
        {
            var realSpeed = new Vector2(Speed, ivRigidbody.velocity.y);

            if (Buffs.IsChilled())
                realSpeed = new Vector2(0.5f * realSpeed.x, realSpeed.y);

            return realSpeed;
        }

        public virtual void Update()
        {
            if (waitingForAnimation || Buffs.IsStunned())
            {
                if(Buffs.IsStunned())
                    OnStunned();

                return;
            }

            if (stunnedLastFrame)
                OnStunExpired();

            ivRigidbody.velocity = new Vector2(0, ivRigidbody.velocity.y);

            if (AiList.Count > 0)
            {
                AiList[0].Invoke();
            }

            ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));         
            
            if(CurrentStats.IsDead())
                HandleDeath();

            stunnedLastFrame = false;
        }

        protected float RangeFromPlayer()
        {
            return Vector2.Distance(transform.position, target.transform.position);
        }

        protected float RangeFromPlayerX()
        {
            return Math.Abs(transform.position.x - target.transform.position.x);
        }

        protected float RangeFromPlayerY()
        {
            return Math.Abs(transform.position.y - target.transform.position.y);
        }

        protected void OnStunExpired()
        {
            AiTimer.Paused = false;
            ivAnimator.enabled = true;
            disabledAttackOnStun.StartMeleeAttack();
            disabledAttackOnStun = null;
        }

        protected void OnStunned()
        {
            if (stunnedLastFrame == false)
            {
                foreach (Attack meleeAttack in GetComponentsInChildren<Attack>())
                {
                    if (meleeAttack.enabled)
                    {
                        meleeAttack.StopMeleeAttack();
                        disabledAttackOnStun = meleeAttack;
                        break;
                    }
                }
                AiTimer.Paused = true;
                ivAnimator.enabled = false;
            }
            stunnedLastFrame = true;
        }

        protected void Flip()
        {
            ivFacingRight = !ivFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        protected void HandleDeath()
        {
            Utility.Logger.Log(string.Format("Gameobject {0} died", gameObject.name));

            GetComponent<ItemDropper>().DropItems();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            player.RewardExp(ExpReward);

            Destroy(gameObject);
        }

        public override void FixedUpdate()
        {
        }

        public void StartWaitingForAnimation()
        {
            waitingForAnimation = true;
        }

        public void StopWaitingForAnimation()
        {
            waitingForAnimation = false;
        }

        public void Disable()
        {
            AiList.Clear();
        }

        public bool IsActive
        {
            get { return AiList.Count > 0; }
        }

        public int ExpReward
        {
            get;
            set;
        }

        protected void GoToState(Action state, bool clearstate = true)
        {
            if(clearstate)
                AiList.RemoveAt(0);
            AiList.Add(state);

        }

        public abstract void TurnAroundCheck();
        public abstract void UpdateTurnAround();
        public abstract void UpdateIdle();
        public abstract void UpdateGoForward();
        public abstract void UpdateAttack();

    }
}
