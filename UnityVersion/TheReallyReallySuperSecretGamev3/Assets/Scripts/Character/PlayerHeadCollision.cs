using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerHeadCollision : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            transform.parent.gameObject.GetComponent<PlatCharController>().TriggerEnterFromHead(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Ground" && transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0f)
            {
                transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}
