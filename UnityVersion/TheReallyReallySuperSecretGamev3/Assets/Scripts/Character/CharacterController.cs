using UnityEngine;
using System.Collections;
using CVCommon.Utility;
using Assets.Scripts.Utility;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour 
{
    public Stats CurrentStats;
    public float speed;
    private bool ivFacingRight;
    private Rigidbody2D ivRigidbody;

	void Start() 
    {
        ivFacingRight = true;
        ivRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
	    
	}

    void FixedUpdate()
    {
        float input = Input.GetAxis("horizontal");

        MovingState moving = MovingState.Still;

        if (input > 0)
            moving = MovingState.Right;
        else if (input < 0)
            moving = MovingState.Left;

        SetMovingState(moving);
    }

    public void SetMovingState(MovingState piState)
    {
        switch (piState)
        {
            case MovingState.Left:
                ivRigidbody.velocity = new Vector2(speed, ivRigidbody.velocity.y);
                if (ivFacingRight)
                    Flip();
                break;
            case MovingState.Right:
                ivRigidbody.velocity = new Vector2(speed, ivRigidbody.velocity.y);
                if (!ivFacingRight)
                    Flip();
                break;
            case MovingState.Still:
                break;
            default:
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
