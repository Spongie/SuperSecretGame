using CVCommon.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheSuperTrueRealCV.EnemyAI
{
    [RequireComponent(typeof(Timer))]
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
