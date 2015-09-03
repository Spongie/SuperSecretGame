using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stat;
using Assets.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(BuffContainer))]
    public class Player : MonoBehaviour, IPlayer
    {
        public PlayerController ivController;

        void Start()
        {
            ivController = new PlayerController(GetComponent<BuffContainer>(), GetComponent<Stats>().stats);
        }

        public void GiveLoot(Item piDrop)
        {
            ivController.GiveLoot(piDrop);
        }

        public void RewardExp(int piAmount)
        {
            ivController.RewardExp(piAmount);
        }

        /// <summary>
        /// Gets base-stats + equipped-stats + Buffstats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return ivController.GetTrueStats();
        }

        public void DealDamage(float amount)
        {
            ivController.DealDamage(amount);
        }

        public void DrainMana(float amount)
        {
            ivController.DrainMana(amount);
        }

        public IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems()
        {
            return ivController.GetAttackEffectsFromEquippedItems();
        }

        public bool LoadPlayer()
        {
            return false;
        }
    }
}
