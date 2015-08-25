using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DamageTextMover : MonoBehaviour
    {
        public float speed;

        void Start()
        {
            GetComponent<Timer>().OnTimerDone += DamageTextMover_OnTimerDone;
        }

        void FixedUpdate()
        {
            transform.position += new Vector3(0, speed, 0);
        }

        private void DamageTextMover_OnTimerDone()
        {
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            GetComponent<Timer>().OnTimerDone -= DamageTextMover_OnTimerDone;
        }
    }
}
