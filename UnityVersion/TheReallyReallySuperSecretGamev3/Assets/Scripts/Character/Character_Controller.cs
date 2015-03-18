using UnityEngine;
using System.Collections;
using CVCommon.Utility;
using Assets.Scripts.Utility;

[RequireComponent(typeof(Rigidbody2D))]
public class Character_Controller : MonoBehaviour 
{
    public Stats CurrentStats;
    public float Speed;
    public float JumpPower;
    public LayerMask GroundLayerMask;
    protected bool ivFacingRight;
    protected Rigidbody2D ivRigidbody;
    private CircleCollider2D ivFeetCollider;
    protected Animator ivAnimator;

	public virtual void Start() 
    {
        CurrentStats = GetComponent<Stats>();
        ivFeetCollider = GetComponent<CircleCollider2D>();
        ivFacingRight = true;
        ivRigidbody = GetComponent<Rigidbody2D>();
        ivAnimator = GetComponent<Animator>();
	}

    public virtual void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();

        MovingState moving = MovingState.Still;

        if (input > 0)
            moving = MovingState.Right;
        else if (input < 0)
            moving = MovingState.Left;

        SetMovingState(moving);
    }

    protected void Jump()
    {
        ivRigidbody.AddForce(Vector2.up * JumpPower);
    }

    public bool IsGrounded()
    {
        var hit = Physics2D.Raycast(ivFeetCollider.transform.position, -Vector2.up, ivFeetCollider.radius + 0.4f, GroundLayerMask);
        Debug.Log((hit.collider != null).ToString());
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
                ivRigidbody.velocity = new Vector2(0, ivRigidbody.velocity.y);
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
