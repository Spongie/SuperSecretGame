using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buffs
{
    public class DoTBuffArea : MonoBehaviour
    {
        public PoisonDebuff buff;
        private List<int> ivGameObjectsInside;

        void Start()
        {
            ivGameObjectsInside = new List<int>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (ivGameObjectsInside.Contains(other.gameObject.GetInstanceID()))
                return;
            
            var buffContainer = other.gameObject.GetComponent<BuffContainer>();
            
            if (buffContainer != null)
                buffContainer.ApplyBuff(buff);

            ivGameObjectsInside.Add(other.gameObject.GetInstanceID());
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var buffContainer = other.gameObject.GetComponent<BuffContainer>();

            if (buffContainer != null)
                buffContainer.ClearBuff(buff);

            ivGameObjectsInside.Remove(other.gameObject.GetInstanceID());
        }
    }
}
