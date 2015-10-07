using UnityEngine;

namespace Assets.Scripts.Utility
{
    [RequireComponent(typeof(Timer))]
    public class KillOnTimer : MonoBehaviour
    {
        private Timer timer;

        void Start()
        {
            timer = GetComponent<Timer>();
        }

        void Update()
        {
            if(timer.Done)
            {
                Destroy(gameObject);
            }
        }
    }
}
