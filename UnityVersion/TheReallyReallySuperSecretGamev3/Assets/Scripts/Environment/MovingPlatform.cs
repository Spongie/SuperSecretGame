using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector2 velocity;
        public Rigidbody2D ivRigidbody;

        void Start()
        {
            ivRigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            ivRigidbody.velocity = velocity;
        }
    }
}
