using Assets.Scripts.Character;
using Assets.Scripts.Utility;
using System.Collections;
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
        public string ActivationButton = "Y";

        void Start()
        {
            ivFadeImage = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>();
            OpenLockTimer = GameObject.FindGameObjectWithTag("Player").GetComponent<Timer>();
        }

        void Update()
        {
            if (Input.GetButtonDown(ActivationButton) && OpenLockTimer.Done && ivCanEnter)
                OpenDoor();
        }

        public void OpenDoor()
        {
            OpenLockTimer.Restart(3);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlatCharController>().OnOpenDoor();
            StartCoroutine("DoorTransition");
        }

        IEnumerator DoorTransition()
        {
            ButtonLock.Instance.AddLock("RB");

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

            Logger.Log("UnFading Screen");

            while (alpha > 0)
            {
                alpha -= 5f;
                ivFadeImage.color = new Color(ivFadeImage.color.r, ivFadeImage.color.g, ivFadeImage.color.b, alpha / 255);
                yield return new WaitForSeconds(0.025f);
            }
            
            ButtonLock.Instance.ClearLock("RB");
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll.gameObject.tag == "Player")
            {
                ivCanEnter = true;
                ButtonLock.Instance.AddLock(ActivationButton);
            }
        }

        void OnTriggerExit2D(Collider2D collisionInfo) 
        {
            if (collisionInfo.gameObject.tag == "Player")
            {
                ivCanEnter = false;
                ButtonLock.Instance.ClearLock(ActivationButton);
            }
        }
    }
}
