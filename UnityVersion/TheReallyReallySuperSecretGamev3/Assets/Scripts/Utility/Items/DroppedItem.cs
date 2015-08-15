using UnityEngine;

namespace Assets.Scripts.Utility.Items
{
    public class DroppedItem : MonoBehaviour
    {
        private SpriteRenderer ivRenderer;
        private GameObject player;
        private BoxCollider2D ivCollider;
        public Item ItemDropped;

        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Items");
            Rigidbody2D rigidB = gameObject.AddComponent<Rigidbody2D>();
            ivCollider = gameObject.AddComponent<BoxCollider2D>();
            ivRenderer = gameObject.AddComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player");
            ivRenderer.material = player.GetComponent<SpriteRenderer>().material;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            SetSprite(ItemDropped.IconName);


            rigidB.velocity = new Vector2(Random.Range(-0.6f, 0.6f), 9);
        }

        private void SetSprite(string piIconName)
        {
            Logger.Log(piIconName);
            ivRenderer.sprite = GameObject.FindGameObjectWithTag("Manager").GetComponent<IconManager>().Icons[piIconName];
            ivCollider.size = ivRenderer.sprite.rect.size / 100;
        }
    }
}
