﻿using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Environment;
using Assets.Scripts.Attacks;

namespace Assets.Scripts.Character
{
    public enum AnimationState
    {
        Idle,
        Running,
        Falling,
        BoostLand,
        Stab,
        Smash,
        BoostJump,
        Jump,
        Dash,
        DiveKick
    }

    public class PlatCharController : MonoBehaviour
    {
        public AnimationState CurrentAnimationState = AnimationState.Idle;
        public string JumpButton = "A";
        public string DashButton = "RB";

        public bool TrulyGrounded = false;
        public bool DiveKickingRight;

        public float IgnoreGravityTime;
        // Horizontal speed
        public float maxSpeed = 8f;
        private float originalMaxSpeed;

        // What we sent the Y velocity to during a jump
        public float jumpSpeed = 12f;

        // Whether our platform will add to jump
        public bool platformRelativeJump = false;

        // Pushing towards a wall will grab on to it.
        public bool allowWallGrab = true;

        // Pushing towards a wall will grab on to it.
        public bool allowWallJump = true;

        // If this is false, we are likely to slide down walls while grabbing (depending on
        // gravity and/or surface friction).  Note that the current circle-based colliders
        // result in weird drifting if you enable this.
        public bool disableGravityDuringWallGrab = false;

        // What layers are valid for us to grab?  For example, we probably can't
        // grab enemies, icy surfaces, etc...  Most walls/floors/platforms
        // should be in the "grabbable" layer(s).
        public LayerMask wallGrabMask;

        // After we wall jump, ignore left/right input for a moment.
        public float wallJumpControlDelay = 0.15f;
        float wallJumpControlDelayLeft = 0;

        // Relative to our transform/pivot point, where are we testing for grabbing?
        // Logically, this should probably be around where the character's hand will
        // be during the grabbing animation.
        public Vector2 grabPoint = new Vector2(0.45f, 0f);

        // Bookkeeping Variables
        MovingPlatform movingPlatform;      // The moving platform we are touching
        bool groundedLastFrame = false;     // Were we grounded last frame? Used by IsGrounded to eliminate top-of-arc issues.
        bool jumping = false;               // Is the player commanding us to jump?
        public bool isJumpControlling = false;
        public bool canDoublejump = false;
        public bool usedDoubleJump = false;
        bool canTriggerJump = true;
        bool stop = false;
        bool dashing = false;
        bool diveKicking = false;

        ManualTimer jumpControlTimer = new ManualTimer(0);
        ManualTimer dashTimer = new ManualTimer(0);
        ManualTimer boostReactionTimer = new ManualTimer(0);
        ManualTimer jumpStartTimer = new ManualTimer(0);
        ManualTimer ignoreTimer = new ManualTimer(0);

        private bool stunnedLastFrame = false;
        private Attack disabledAttackOnStun;
        private bool ivWaitingForAnimation = false;

        private GameObject ivLastSlop;
        public bool ivMovedLastFrame;

        Rigidbody2D ivRigidbody;
        ManualTimer ivGravityTimer;

        public PhysicsMaterial2D SlopeMaterial;
        public PhysicsMaterial2D WallMaterial;

        private CircleCollider2D ivFeetCollider;

        public LayerMask OriginalLayer;
        public LayerMask IgnoringLayer;

        private bool boostingRight = false;
        private Player ivPlayer;
        private Animator ivAnimator;

        // Use this for initialization
        void Start()
        {
            ivRigidbody = GetComponent<Rigidbody2D>();
            ivGravityTimer = new ManualTimer(0);
            originalMaxSpeed = maxSpeed;
            ivFeetCollider = GetComponent<CircleCollider2D>();
            ivPlayer = GetComponent<Player>();
            ivAnimator = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // If we collided against a platform, grab a copy of it and we can use it as our zero point for IsGrounded.
            MovingPlatform mp = col.transform.root.GetComponent<MovingPlatform>();
            if (mp != null)
            {
                movingPlatform = mp;
            }

            if (col.gameObject.tag.Contains("Boost") && diveKicking)
            {
                if (col.collider.transform.position.y <= ivFeetCollider.bounds.center.y) //ivFeetCollider.bounds.center.y - ivFeetCollider.radius)
                {
                    bool landedOnRightBoost = col.gameObject.tag == "BoostRight";

                    if (landedOnRightBoost == DiveKickingRight)
                    {
                        boostReactionTimer.Restart(2);
                        stop = true;
                        Logger.Log("Landed on boost");
                        boostingRight = landedOnRightBoost;
                        ivAnimator.SetBool("BoostLand", true);
                        CurrentAnimationState = AnimationState.BoostLand;
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Ground")
            {
                if (other.isTrigger && ignoreTimer.Done)
                {
                    Logger.Log("Hit Ignore trigger");
                    gameObject.layer = LayerMask.NameToLayer("IgnoreGround");
                    ignoreTimer.Restart(0.1f);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Ground")
            {
                Logger.Log("Head not touching roof");
                canTriggerJump = true;
            }
        }

        /// <summary>
        /// Determines if the character is grounded based on having a zero velocity relative to his platform.
        /// </summary>
        bool IsGrounded()
        {
            if (Mathf.Abs(RelativeVelocity().y) < 0.1f && !isJumpControlling && !jumping)
            {   // Checking floats for exact equality is bad. Also good for platforms that are moving down.

                // Since it's possible for our velocity to be exactly zero at the apex of our jump,
                // we actually require two zero velocity frames in a row.

                if (groundedLastFrame)
                {
                    setGrounded();
                    return true;
                }

                Logger.Log("Grounded first frame");

                groundedLastFrame = true;
            }
            else
            {
                if (!jumpStartTimer.Done)
                    return false;

                var from = GetComponent<CircleCollider2D>().bounds.center;

                if (Debug.isDebugBuild)
                    Debug.DrawRay(from, new Vector3(0, -0.35f, 0), Color.green);


                var hits = Physics2D.RaycastAll(from, new Vector2(0, -1), 0.35f);

                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.name == "Player")
                            continue;

                        if (hit.transform.rotation.z != 0)
                        {
                            Logger.Log("Im moving");
                            ivLastSlop = hit.collider.gameObject;
                            transform.position = new Vector2(transform.position.x, hit.point.y);
                            if (groundedLastFrame)
                            {
                                setGrounded();
                                return true;
                            }

                            Logger.Log("Grounded first frame");
                            groundedLastFrame = true;

                            return false;
                        }
                    }

                }

                groundedLastFrame = false;
                TrulyGrounded = false;
            }

            return false;
        }

        public void OnOpenDoor()
        {
            setGrounded();
            ivRigidbody.velocity = Vector2.zero;
        }

        private void setGrounded()
        {
            if (TrulyGrounded)
                return;

            Logger.Log("Setting grounded");
            usedDoubleJump = false;
            canDoublejump = true;
            diveKicking = false;
            maxSpeed = originalMaxSpeed;
            TrulyGrounded = true;
            LandCancel();
        }

        private void LandCancel()
        {
            if (!boostReactionTimer.Done)
                return;

            Logger.Log("Doing a Land-Cancel");
            foreach (Attack attack in GetComponentsInChildren<Attack>())
            {
                attack.StopMeleeAttack();
            }

            ResetAnimation();
        }

        private void ResetAnimation()
        {
            CurrentAnimationState = AnimationState.Idle;
            ivAnimator.SetBool("Idle", true);
            ivAnimator.SetBool("Running", false);
            ivAnimator.SetBool("Falling", false);
        }

        /// <summary>
        /// Determines if we're grabbing a surface.
        /// </summary>
        bool IsGrabbing()
        {
            if (allowWallGrab == false)
                return false;

            // FIXME: Is there any chance we want to set movingPlatform here?

            // If we're pushing the joystick in the direction of our facing and an OverlapCircle test indicates a grabbable surface at the grabPoint, return true.
            return ((Input.GetAxisRaw("Horizontal") > 0 && this.transform.localScale.x > 0) || (Input.GetAxisRaw("Horizontal") < 0 && this.transform.localScale.x < 0)) &&
                Physics2D.OverlapCircle(this.transform.position + new Vector3(grabPoint.x * this.transform.localScale.x, grabPoint.y, 0), 0.2f, wallGrabMask);
        }

        private void OnStunExpired()
        {
            //ivAnimator.enabled = true;
            disabledAttackOnStun.StartMeleeAttack();
            disabledAttackOnStun = null;
        }

        public void CancelActiveAttack()
        {
            foreach (Attack meleeAttack in GetComponentsInChildren<Attack>())
            {
                if (meleeAttack.enabled)
                {
                    meleeAttack.StopMeleeAttack();
                }
            }

            ivWaitingForAnimation = false;
        }

        private void OnStunned()
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

                //ivAnimator.enabled = false;
            }
            stunnedLastFrame = true;
        }

        void Update()
        {
            if (ivWaitingForAnimation)
                return;

            if(ivPlayer.ivController.GetBuffContainer().IsStunned())
            {
                OnStunned();
                return;
            }

            if (stunnedLastFrame)
                OnStunExpired();

            stunnedLastFrame = false;

            jumpControlTimer.Update(Time.deltaTime);
            dashTimer.Update(Time.deltaTime);
            boostReactionTimer.Update(Time.deltaTime);
            jumpStartTimer.Update(Time.deltaTime);
            ignoreTimer.Update(Time.deltaTime);

            if(Input.GetButtonDown("X"))
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
                {
                    CurrentAnimationState = AnimationState.Stab;
                    ivAnimator.SetTrigger("Stab");
                }
                else
                {
                    CurrentAnimationState = AnimationState.Smash;
                    ivAnimator.SetTrigger("Smash");
                }

                ivWaitingForAnimation = true;
            }

            if (CanMove() && canTriggerJump)
                CheckJump();

            if (!boostReactionTimer.Done && Mathf.Abs(ivRigidbody.velocity.y) > 0.3f)
            {
                boostReactionTimer.Cancel();
                ivAnimator.SetBool("Running", true);
                CurrentAnimationState = AnimationState.Running;
            }
            if (Input.GetButtonDown(DashButton) && !dashing && !diveKicking && !ButtonLock.Instance.IsButtonLocked(DashButton))
            {
                if (!boostReactionTimer.Done)
                {
                    Logger.Log("Activated boost");
                    maxSpeed = 10;
                    ivRigidbody.velocity = new Vector2(SetDashSpeed(maxSpeed), 4);
                    boostReactionTimer.Cancel();
                    CurrentAnimationState = AnimationState.BoostJump;
                    ivAnimator.SetBool("BoostLand", false);
                    ivAnimator.SetBool("BoostJump", true);
                }
                else
                {
                    if (IsGrounded())
                    {
                        dashTimer.Restart(0.15f);
                        dashing = true;
                        ivAnimator.SetTrigger("Dash");
                        CurrentAnimationState = AnimationState.Dash;
                    }
                    else
                    {
                        diveKicking = true;
                        ivAnimator.SetTrigger("DiveKick");
                        CurrentAnimationState = AnimationState.DiveKick;
                        DiveKickingRight = transform.localScale.x < 0 ? false : true;
                    }
                }

            }

            if (Input.GetAxisRaw("Horizontal") == 0)
                maxSpeed = originalMaxSpeed;
        }

        private void CheckJump()
        {
            if (!ignoreTimer.Done)
                return;

            if (Input.GetButton(JumpButton) && !ButtonLock.Instance.IsButtonLocked(JumpButton))
            {
                bool grounded = IsGrounded();
                if (grounded || canDoublejump)
                {
                    if (Input.GetAxisRaw("Vertical") < 0 && grounded)
                    {
                        gameObject.layer = LayerMask.NameToLayer("IgnoreGround");
                        ignoreTimer.Restart(0.4f);
                        return;
                    }

                    if (!isJumpControlling)
                        jumpControlTimer.Restart(0.3f);

                    if (!IsGrounded() && !isJumpControlling)
                    {
                        usedDoubleJump = true;
                        canDoublejump = false;
                    }
                    isJumpControlling = true;
                }

                jumping = true;
                ivAnimator.SetBool("Jump", true);
                CurrentAnimationState = AnimationState.Jump;
            }
            else
            {
                isJumpControlling = false;
            }

            if (!Input.GetButton(JumpButton) && !isJumpControlling && !usedDoubleJump && !ButtonLock.Instance.IsButtonLocked(JumpButton))
            {
                canDoublejump = true;
            }
            else
                canDoublejump = false;
        }

        /// <summary>
        /// Our velocity relative to the platform we're on, if any.
        /// </summary>
        /// <returns>The velocity.</returns>
        Vector2 RelativeVelocity()
        {
            return ivRigidbody.velocity - PlatformVelocity();
        }

        /// <summary>
        /// The velocity of the platform we're on (or zero)
        /// </summary>
        /// <returns>The velocity.</returns>
        Vector2 PlatformVelocity()
        {
            if (movingPlatform == null)
                return Vector2.zero;

            return movingPlatform.ivRigidbody.velocity;
        }

        // Update is called once per physics loop
        void FixedUpdate()
        {
            if (ivWaitingForAnimation)
                return;

            ivGravityTimer.Update(Time.deltaTime);

            bool isGrounded = IsGrounded();
            bool isGrabbing = !isGrounded && wallJumpControlDelayLeft <= 0 && IsGrabbing();

            if (movingPlatform != null && !groundedLastFrame && !isGrabbing && !isGrounded)
            {
                // We aren't grounded or grabbing.  Making sure to clear our platform.
                movingPlatform = null;
            }

            // FIXME: This results in weird drifting with our current colliders
            if (disableGravityDuringWallGrab)
            {
                if (isGrabbing)
                    ivRigidbody.gravityScale = 0;
                else
                    ivRigidbody.gravityScale = 1;
            }

            // We start off by assuming we are maintaining our velocity.
            float xVel = ivRigidbody.velocity.x;
            float yVel = ivRigidbody.velocity.y;

            // If we're grounded, maintain our velocity at platform velocity, with slight downward pressure to maintain the collision.
            if (isGrounded)
            {
                yVel = PlatformVelocity().y - 0.01f;
            }

            // Some moves (like walljumping) might introduce a delay before x-velocity is controllable
            wallJumpControlDelayLeft -= Time.deltaTime;

            if (isGrounded || isGrabbing)
            {
                wallJumpControlDelayLeft = 0;   // Clear the delay if we're in contact with the ground/wall
            }

            // Allow x-velocity control
            if (wallJumpControlDelayLeft <= 0)
            {
                if (CanMove())
                    xVel = Input.GetAxisRaw("Horizontal") * maxSpeed;

                var xMove = Input.GetAxisRaw("Horizontal");

                if(!boostReactionTimer.Done && Mathf.Abs(xMove) > 0)
                {
                    Logger.Log(string.Format("Trying to cancel boost, BoostingRight: {0}        xVel: {1}", boostingRight, xMove));
                    if (xMove < 0 && boostingRight)
                    {
                        boostReactionTimer.Cancel();
                        ivAnimator.SetBool("BoostLand", false);
                    }
                    else if (xMove > 0 && !boostingRight)
                    {
                        boostReactionTimer.Cancel();
                        ivAnimator.SetBool("BoostLand", false);
                    }
                }

                xVel += PlatformVelocity().x;
                if (ivLastSlop != null)
                    Debug.Log(string.Format("XVel:{0}     Slope:{1}", xVel, ivLastSlop.name));

                if (xVel == 0 && ivMovedLastFrame)
                {
                    if (ivLastSlop != null)
                        setLastSlopePhysicsMaterial(SlopeMaterial);
                }
                else if (xVel > 0 && !ivMovedLastFrame)
                {
                    if (ivLastSlop != null)
                        setLastSlopePhysicsMaterial(WallMaterial);
                }
            }

            if (isGrabbing && RelativeVelocity().y <= 0)
            {
                // NOTE:  Depending on friction and gravity, the character
                // will still be sliding down unless we turn off gravityScale
                yVel = PlatformVelocity().y;

                // If we are are zero velocity (or "negative" relative to the facing of the wall)
                // set our velocity to zero so we don't bounce off
                // Also ensures that we don't inter-penetrate for one frame, which could cause us
                // to get stuck on a "ledge" between blocks.
                if (RelativeVelocity().x * transform.localScale.x <= 0)
                {
                    xVel = PlatformVelocity().x;
                }
            }

            if (jumping && (isGrounded || (isGrabbing && allowWallJump)))
            {
                // NOTE: As-is, neither vertical velocity nor walljump speed is affected by PlatformVelocity().
                isJumpControlling = true;
                jumpStartTimer.Restart(0.1f);

                yVel = jumpSpeed;
                if (platformRelativeJump)
                    yVel += PlatformVelocity().y;

                if (isGrabbing)
                {
                    xVel = -maxSpeed * this.transform.localScale.x;
                    wallJumpControlDelayLeft = wallJumpControlDelay;
                }
            }

            if (isJumpControlling && !jumpControlTimer.Done)
                yVel = jumpSpeed;
            else
                isJumpControlling = false;

            jumping = false;
            dashing = !dashTimer.Done;

            if (!dashing)
            {
                if (xVel > 0.05)
                    xVel = maxSpeed;
                else if (xVel < -0.05f)
                    xVel = -maxSpeed;
            }
            else
            {
                xVel = SetDashSpeed(xVel);
            }

            if (diveKicking)
            {
                xVel = SetDashSpeed(xVel);
                yVel = -jumpSpeed;
            }

            if (stop)
            {
                yVel = 0;
                xVel = 0;
                stop = false;
            }

            if (Mathf.Abs(xVel) > 0)
            {
                ivMovedLastFrame = true;
            }
            else
                ivMovedLastFrame = false;

            if(ivPlayer.ivController.GetBuffContainer().IsChilled())
                xVel /= 2;

            if (yVel > 8)
                yVel = 8;

            // Apply the calculate velocity to our rigidbody
            ivRigidbody.velocity = new Vector2(
                    xVel,
                    yVel
                );

            if (Mathf.Abs(xVel) > 0.1 && !dashing && !diveKicking)
            {
                CurrentAnimationState = AnimationState.Running;
                ivAnimator.SetBool("Running", true);
            }
            else
                ivAnimator.SetBool("Running", false);

            if (yVel < -0.02f)
            {
                if (CurrentAnimationState != AnimationState.Falling)
                {
                    CurrentAnimationState = AnimationState.Falling;
                    ivAnimator.SetBool("Jump", false);
                    ivAnimator.SetBool("Running", false);
                    ivAnimator.SetBool("BoostJump", false);
                    ivAnimator.SetBool("Falling", true);
                }
            }
            else
                ivAnimator.SetBool("Falling", false);

            if (xVel >= -0.01f && xVel <= 0.01f && yVel >= -0.01f && yVel <= 0.01f)
            {
                if (CurrentAnimationState != AnimationState.Idle)
                {
                    CurrentAnimationState = AnimationState.Idle;
                    ivAnimator.SetBool("Idle", true);
                    ivAnimator.SetBool("Falling", false);
                    ivAnimator.SetBool("Running", false);

                    if (boostReactionTimer.Done)
                        ivAnimator.SetBool("BoostLand", false);
                }
            }
            else
                ivAnimator.SetBool("Idle", false);

            if (ivRigidbody.velocity.y < 0 && ignoreTimer.Done)
                gameObject.layer = LayerMask.NameToLayer("Default");

            if (CanMove())
            {
                // Update facing
                Vector3 scale = this.transform.localScale;
                if (scale.x < 0 && Input.GetAxis("Horizontal") > 0)
                {
                    scale.x = 0.3f;
                    maxSpeed = originalMaxSpeed;
                }
                else if (scale.x > 0 && Input.GetAxis("Horizontal") < 0)
                {
                    scale.x = -0.3f;
                    maxSpeed = originalMaxSpeed;
                }
                this.transform.localScale = scale;
            }
        }

        private void setLastSlopePhysicsMaterial(PhysicsMaterial2D piMaterial)
        {
            ivLastSlop.GetComponent<BoxCollider2D>().sharedMaterial = piMaterial;
            ivLastSlop.GetComponent<BoxCollider2D>().enabled = false;
            ivLastSlop.GetComponent<BoxCollider2D>().enabled = true;
        }

        private float SetDashSpeed(float xVel)
        {
            xVel = maxSpeed * 2;

            if (transform.localScale.x == -0.3f)
                xVel *= -1;
            return xVel;
        }

        private bool CanMove()
        {
            return !dashing && !diveKicking && boostReactionTimer.Done;
        }
    }
}