using UnityEngine;
using System.Linq;
using Assets.Scripts.Character;

namespace Assets.Scripts.Utility.Items
{
    public class DroppedItem : MonoBehaviour
    {
        private const float cLootDistance = 3.86f;
        private SpriteRenderer ivRenderer;
        private GameObject player;
        private BoxCollider2D ivCollider;
        private Transform ivLootCenter;
        public Item ItemDropped;
        public bool CanBeLooted = false;
        Rigidbody2D rigidB;

        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Items");
            rigidB = gameObject.AddComponent<Rigidbody2D>();
            ivCollider = gameObject.AddComponent<BoxCollider2D>();
            ivRenderer = gameObject.AddComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player");
            ivLootCenter = player.GetComponentsInChildren<Transform>().First(position => position.name == "LootCenter");
            ivRenderer.material = player.GetComponent<SpriteRenderer>().material;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            SetSprite(ItemDropped.IconName);


            rigidB.velocity = new Vector2(Random.Range(-0.6f, 0.6f), 9);
        }

        void Update()
        {
            if (rigidB.velocity.y <= 0)
                CanBeLooted = true;

            if (CanBeLooted)
            {
                float distance = Mathf.Abs(Vector3.Distance(ivLootCenter.position, transform.position));
                
                if (distance <= cLootDistance)
                {
                    player.GetComponent<Player>().GiveLoot(ItemDropped);
                    Destroy(gameObject);
                }
            }
        }

        private void SetSprite(string piIconName)
        {
            Logger.Log(piIconName);
            ivRenderer.sprite = GameObject.FindGameObjectWithTag("Manager").GetComponent<IconManager>().Icons[piIconName];
            ivCollider.size = ivRenderer.sprite.rect.size / 100;
        }
    }
}
