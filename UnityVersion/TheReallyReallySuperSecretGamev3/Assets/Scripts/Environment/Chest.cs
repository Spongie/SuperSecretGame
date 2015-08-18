using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    [RequireComponent(typeof(ItemDropper))]
    public class Chest : MonoBehaviour
    {
        private bool ivCanOpen;
        public string ActivationButton = "A";

        void Update()
        {
            if (Input.GetButtonDown(ActivationButton) && ivCanOpen)
            {
                KillChest();
            }
        }


        private void KillChest()
        {
            GetComponent<ItemDropper>().DropItems();
            ButtonLock.Instance.ClearLock(ActivationButton);
            Destroy(gameObject);
        }


        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                ivCanOpen = true;
                ButtonLock.Instance.AddLock(ActivationButton);
            }
        }

        void OnTriggerExit2D(Collider2D collisionInfo)
        {
            if (collisionInfo.gameObject.tag == "Player")
            {
                ivCanOpen = false;
                ButtonLock.Instance.ClearLock(ActivationButton);
            }
        }
    }
}
