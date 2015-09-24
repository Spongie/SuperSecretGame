using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class EnableCopyer : MonoBehaviour
    {
        public GameObject Target;

        void Start()
        {
        }

        IEnumerator Copy()
        {
            while (true)
            {
                bool targetStatus = Target.activeSelf;

                if (targetStatus != gameObject.activeSelf)
                    gameObject.SetActive(targetStatus);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
