using Assets.Scripts.Buffs;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Monsters;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheSuperTrueRealCV.EnemyAI
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
        private BuffContainer Buffs;

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
            Buffs.Stats = CurrentStats.stats;
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

        public virtual void Update()
        {
            if (waitingForAnimation)
                return;

            ivRigidbody.velocity = new Vector2(0, ivRigidbody.velocity.y);

            if (AiList.Count > 0)
            {
                AiList[0].Invoke();
            }

            ivAnimator.SetFloat("Speed", Mathf.Abs(ivRigidbody.velocity.x));         
            
            if(CurrentStats.IsDead())
                HandleDeath();
        }

        private void HandleDeath()
        {
            var drops = GetComponent<ItemDropper>().GetDroppedItems();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            player.GiveLoot(drops);
            player.RewardExp(ExpReward);

            Destroy(gameObject);
        }

        public override void FixedUpdate()
        {
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

        public abstract void TurnAroundCheck();
        public abstract void UpdateTurnAround();
        public abstract void UpdateIdle();
        public abstract void UpdateGoForward();
        public abstract void UpdateAttack();

    }
}
