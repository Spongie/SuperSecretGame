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
            if ((other.tag == "Ground" || other.tag.ToLower().StartsWith("boost")) && transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0f)
            {
                transform.parent.gameObject.GetComponent<PlatCharController>().TriggerExitFromHead(other);
            }
        }

        
    }
}
