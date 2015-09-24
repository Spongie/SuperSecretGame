using Assets.Scripts.Character.Monsters;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class AIEnabler : MonoBehaviour
    {
        public GameObject Player;

        void Update()
        {
            transform.position = Player.transform.position;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var monster = other.gameObject.GetComponent<Monster>();

            if (monster != null)
            {
                monster.Activate();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var monster = other.gameObject.GetComponent<Monster>();

            if (monster != null)
            {
                monster.Deactivate();
            }
        }
    }
}
