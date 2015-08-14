using UnityEngine;
using Assets.Scripts.Utility;

public class PlatCharController : MonoBehaviour
{
    public string JumpButton = "A";
    public string DashButton = "RB";
    
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
    MovingPlatform movingPlatform;		// The moving platform we are touching
    bool groundedLastFrame = false;		// Were we grounded last frame? Used by IsGrounded to eliminate top-of-arc issues.
    bool jumping = false;				// Is the player commanding us to jump?
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

    private GameObject ivLastSlop;
    public bool ivMovedLastFrame;

    Rigidbody2D ivRigidbody;
    ManualTimer ivGravityTimer;

    public PhysicsMaterial2D SlopeMaterial;
    public PhysicsMaterial2D WallMaterial;

    private CircleCollider2D ivFeetCollider;

    // Use this for initialization
    void Start()
    {
        ivRigidbody = GetComponent<Rigidbody2D>();
        ivGravityTimer = new ManualTimer(0);
        originalMaxSpeed = maxSpeed;
        ivFeetCollider = GetComponent<CircleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If we collided against a platform, grab a copy of it and we can use it as our zero point for IsGrounded.
        MovingPlatform mp = col.transform.root.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            Logger.Log("movingPlatform: " + mp.gameObject.name);
            movingPlatform = mp;
        }

        if(col.gameObject.tag == "Boost" && diveKicking)
        {
            if (col.collider.transform.position.y <= ivFeetCollider.bounds.center.y - ivFeetCollider.radius)
            {
              boostReactionTimer.Restart(2);
              stop = true;
              Logger.Log("Landed on boost");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            Logger.Log("Hit ground with head");
            isJumpControlling = false;
            jumping = false;
            canTriggerJump = false;
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
        {	// Checking floats for exact equality is bad. Also good for platforms that are moving down.

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

            if(Debug.isDebugBuild)
                Debug.DrawRay(from, new Vector3(0, -0.23f, 0), Color.green);

            var hits = Physics2D.RaycastAll(from, new Vector2(0, -1), 0.23f);

            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == "Player")
                        continue;

                    if(hit.transform.rotation.z != 0)
                    {
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
        usedDoubleJump = false;
        canDoublejump = true;
        diveKicking = false;
        maxSpeed = originalMaxSpeed;
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

    void Update()
    {
        jumpControlTimer.Update(Time.deltaTime);
        dashTimer.Update(Time.deltaTime);
        boostReactionTimer.Update(Time.deltaTime);
        jumpStartTimer.Update(Time.deltaTime);

        if (CanMove() && canTriggerJump)
            CheckJump();

        if (!boostReactionTimer.Done && Mathf.Abs(ivRigidbody.velocity.y) > 0.3f)
            boostReactionTimer.Restart(0);

        if (Input.GetButtonDown(DashButton) && !dashing && !diveKicking && !ButtonLock.Instance.IsButtonLocked(DashButton))
        {
            if (!boostReactionTimer.Done)
            {
                Logger.Log("Activated boost");
                maxSpeed = 10;
                ivRigidbody.velocity = new Vector2(SetDashSpeed(maxSpeed), 4);
                boostReactionTimer.Restart(0);
            }
            else
            {
                if (IsGrounded())
                {
                    dashTimer.Restart(0.15f);
                    dashing = true;
                }
                else
                {
                    diveKicking = true;
                }
            }

        }

        if (Input.GetAxisRaw("Horizontal") == 0)
            maxSpeed = originalMaxSpeed;
    }

    private void CheckJump()
    {
        if (Input.GetButton(JumpButton) && !ButtonLock.Instance.IsButtonLocked(JumpButton))
        {
            if (IsGrounded() || canDoublejump)
            {
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
            wallJumpControlDelayLeft = 0;	// Clear the delay if we're in contact with the ground/wall
        }

        // Allow x-velocity control
        if (wallJumpControlDelayLeft <= 0)
        {
            if (CanMove())
                xVel = Input.GetAxisRaw("Horizontal") * maxSpeed;

            xVel += PlatformVelocity().x;
            if(ivLastSlop != null)
                Debug.Log(string.Format("XVel:{0}     Slope:{1}", xVel, ivLastSlop.name));

            if (xVel == 0 && ivMovedLastFrame)
            {
                if (ivLastSlop != null)
                    setLastSlopePhysicsMaterial(SlopeMaterial);
            }
            else if(xVel > 0 && !ivMovedLastFrame)
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

        if(diveKicking)
        {
            xVel = SetDashSpeed(xVel);
            yVel = -jumpSpeed;
        }

        if(stop)
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

        // Apply the calculate velocity to our rigidbody
        ivRigidbody.velocity = new Vector2(
                xVel,
                yVel
            );

        if (CanMove())
        {
            // Update facing
            Vector3 scale = this.transform.localScale;
            if (scale.x < 0 && Input.GetAxis("Horizontal") > 0)
            {
                scale.x = 1;
                maxSpeed = originalMaxSpeed;
            }
            else if (scale.x > 0 && Input.GetAxis("Horizontal") < 0)
            {
                scale.x = -1;
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

        if (transform.localScale.x == -1)
            xVel *= -1;
        return xVel;
    }

    private bool CanMove()
    {
        return !dashing && !diveKicking && boostReactionTimer.Done;
    }
}
