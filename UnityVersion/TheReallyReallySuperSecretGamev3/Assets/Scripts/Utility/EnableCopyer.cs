using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utility
{
    public class EnableCopyer : MonoBehaviour
    {
        public GameObject Target;

        void Start()
        {
            StartCoroutine(Copy());
        }

        IEnumerator Copy()
        {
            Image image = GetComponent<Image>();
            while (true)
            {
                bool targetStatus = Target.activeSelf;

                if (targetStatus != image.IsActive())
                    image.enabled = targetStatus;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
