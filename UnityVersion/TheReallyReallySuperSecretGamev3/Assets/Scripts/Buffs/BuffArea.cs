using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buffs
{
    public enum BuffAreaOwner
    {
        Player,
        Monster,
        World
    }

    [RequireComponent(typeof(Timer))]
    public class BuffArea : MonoBehaviour
    {
        public BuffAreaOwner Owner;
        public Buff[] Buffs;
        public PoisonDebuff[] DoTDebuff;
        public ManaDrainBuff[] ManaDBuff;

        private Timer LifeTimer;
        private List<int> ivGameObjectsInside;

        void Start()
        {
            ivGameObjectsInside = new List<int>();
            LifeTimer = GetComponent<Timer>();
        }

        void Update()
        {
            if (LifeTimer.Done)
                Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (HitOwner(other))
                return;

            if (ivGameObjectsInside.Contains(other.gameObject.GetInstanceID()))
                return;

            var buffContainer = other.gameObject.GetComponent<BuffContainer>();

            if (buffContainer != null)
                AddAllBuffs(buffContainer);

            ivGameObjectsInside.Add(other.gameObject.GetInstanceID());
        }

        private bool HitOwner(Collider2D other)
        {
            return Enum.GetName(typeof(BuffAreaOwner), Owner) == other.gameObject.tag;
        }

        private void AddAllBuffs(BuffContainer buffContainer)
        {
            foreach (var buff in Buffs)
            {
                buffContainer.ApplyBuff(buff);
            }

            foreach (var buff in DoTDebuff)
            {
                buffContainer.ApplyBuff(buff);
            }

            foreach (var buff in ManaDBuff)
            {
                buffContainer.ApplyBuff(buff);
            }
        }

        private void ClearAllBuffs(BuffContainer buffContainer)
        {
            foreach (var buff in Buffs)
            {
                buffContainer.ClearBuff(buff);
            }

            foreach (var buff in DoTDebuff)
            {
                buffContainer.ClearBuff(buff);
            }

            foreach (var buff in ManaDBuff)
            {
                buffContainer.ClearBuff(buff);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var buffContainer = other.gameObject.GetComponent<BuffContainer>();

            if (buffContainer != null)
                ClearAllBuffs(buffContainer);

            ivGameObjectsInside.Remove(other.gameObject.GetInstanceID());
        }
    }
}
