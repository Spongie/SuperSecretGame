﻿using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Character.Stats;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character_Controller : MonoBehaviour
    {
        public float IgnoreGravityTime;
        public EntityStats CurrentStats;
        public float Speed;
        public float JumpPower;
        public LayerMask GroundLayerMask;
        public bool ivFacingRight;
        protected Rigidbody2D ivRigidbody;
        private CircleCollider2D ivFeetCollider;
        protected Animator ivAnimator;

        float ivOriginalGravity;
        ManualTimer ivGravityTimer;

        public virtual void Start()
        {
            CurrentStats = GetComponent<EntityStats>();
            ivFeetCollider = GetComponent<CircleCollider2D>();
            ivFacingRight = true;
            ivRigidbody = GetComponent<Rigidbody2D>();
            ivAnimator = GetComponent<Animator>();
            ivOriginalGravity = ivRigidbody.gravityScale;
            ivGravityTimer = new ManualTimer(0);
        }

        public virtual void FixedUpdate()
        {
            ivGravityTimer.Update(Time.deltaTime);

            float input = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                Jump();

            MovingState moving = MovingState.Still;

            if (ivGravityTimer.Done)
                ivRigidbody.gravityScale = ivOriginalGravity;

            if (input > 0)
                moving = MovingState.Right;
            else if (input < 0)
                moving = MovingState.Left;
            else if (ivRigidbody.velocity.y > 0)
                ivRigidbody.velocity = new Vector2(0, ivRigidbody.velocity.y);

            SetMovingState(moving);
        }

        protected void Jump()
        {
            ivRigidbody.velocity = new Vector2(ivRigidbody.velocity.x, JumpPower);

            ivGravityTimer.Restart(IgnoreGravityTime);
            ivRigidbody.gravityScale = 0f;
        }

        public bool IsGrounded()
        {
            var hit = Physics2D.Raycast(ivFeetCollider.transform.position, -Vector2.up, ivFeetCollider.radius + 0.4f, GroundLayerMask);
            return hit.collider != null;
        }

        public void SetMovingState(MovingState piState)
        {
            switch (piState)
            {
                case MovingState.Left:
                    ivRigidbody.velocity = new Vector2(-Speed, ivRigidbody.velocity.y);
                    if (ivFacingRight)
                        Flip();
                    break;
                case MovingState.Right:
                    ivRigidbody.velocity = new Vector2(Speed, ivRigidbody.velocity.y);
                    if (!ivFacingRight)
                        Flip();
                    break;
                case MovingState.Still:
                    break;
            }
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