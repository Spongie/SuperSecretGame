using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Environment
{
    [RequireComponent(typeof(Timer))]
    public class Door : MonoBehaviour
    {
        public Transform Target;
        private Image ivFadeImage;
        private bool ivCanEnter = false;
        private Timer OpenLockTimer;

        void Start()
        {
            ivFadeImage = FindObjectsOfType<Image>().Where(image => image.name == "FadeImage").First();
            OpenLockTimer = GetComponent<Timer>();
        }

        void Update()
        {
            if (Input.GetButton("Fire1") && OpenLockTimer.Done)
                OpenDoor();
        }

        public void OpenDoor()
        {
            OpenLockTimer.Restart(3);
            StartCoroutine("DoorTransition");
        }

        IEnumerable DoorTransition()
        {
            while(ivFadeImage.color.a < 255)
            {
                ivFadeImage.color = new Color(ivFadeImage.color.r, ivFadeImage.color.g, ivFadeImage.color.b, ivFadeImage.color.a + 15);
                yield return new WaitForSeconds(0.05f);
            }

            GameObject.FindGameObjectWithTag("Player").transform.position = Target.position;

            while (ivFadeImage.color.a > 0)
            {
                ivFadeImage.color = new Color(ivFadeImage.color.r, ivFadeImage.color.g, ivFadeImage.color.b, ivFadeImage.color.a - 15);
                yield return new WaitForSeconds(0.05f);
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll.gameObject.tag == "Player")
            {
                ivCanEnter = true;
            }
        }

        void OnCollisionExit(Collision collisionInfo) 
        {
            if (collisionInfo.gameObject.tag == "Player")
            {
                ivCanEnter = false;
            }
        }
    }
}
