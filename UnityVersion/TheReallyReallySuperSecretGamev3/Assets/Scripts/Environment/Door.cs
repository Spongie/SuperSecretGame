using Assets.Scripts.Utility;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Environment
{
    public class Door : MonoBehaviour
    {
        public Transform Target;
        private Image ivFadeImage;
        private bool ivCanEnter = false;
        public Timer OpenLockTimer;

        void Start()
        {
            ivFadeImage = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>();
            OpenLockTimer = GameObject.FindGameObjectWithTag("Player").GetComponent<Timer>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Y") && OpenLockTimer.Done && ivCanEnter)
                OpenDoor();
        }

        public void OpenDoor()
        {
            OpenLockTimer.Restart(3);
            StartCoroutine("DoorTransition");
        }

        IEnumerator DoorTransition()
        {
            float alpha = 0f;
            Logger.Log("Fading Screen");
            while (alpha < 255)
            {
                alpha += 20f;
                ivFadeImage.color = new Color(ivFadeImage.color.r, ivFadeImage.color.g, ivFadeImage.color.b, alpha / 255);
                yield return new WaitForSeconds(0.025f);
            }

            Logger.Log("Moving player");
            GameObject.FindGameObjectWithTag("Player").transform.position = Target.position;

            while (alpha > 0)
            {
                alpha -= 5f;
                ivFadeImage.color = new Color(ivFadeImage.color.r, ivFadeImage.color.g, ivFadeImage.color.b, alpha / 255);
                yield return new WaitForSeconds(0.025f);
            }
            Logger.Log("UnFading Screen");
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll.gameObject.tag == "Player")
            {
                ivCanEnter = true;
            }
        }

        void OnTriggerExit2D(Collider2D collisionInfo) 
        {
            if (collisionInfo.gameObject.tag == "Player")
            {
                ivCanEnter = false;
            }
        }
    }
}
