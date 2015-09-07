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
        public GameObject MenuController;
        public int InventoryItems;

        public PlayerController Controller { get; private set; }

        void Start()
        {
            Controller = new PlayerController(GetComponent<BuffContainer>(), GetComponent<Stats>().stats);
        }

        public void GiveLoot(Item piDrop)
        {
            Controller.GiveLoot(piDrop);
        }

        public void RewardExp(int piAmount)
        {
            Controller.RewardExp(piAmount);
        }

        void Update()
        {
            InventoryItems = Controller.PlayerInventory.Items.Count;

            if(Input.GetKeyDown(KeyCode.O))
            {
                Time.timeScale = 0;
                MenuController.SetActive(true);
            }
        }

        /// <summary>
        /// Gets base-stats + equipped-stats + Buffstats
        /// </summary>
        /// <returns></returns>
        public CStats GetTrueStats()
        {
            return Controller.GetTrueStats();
        }

        public void DealDamage(float amount)
        {
            Controller.DealDamage(amount);
        }

        public void DrainMana(float amount)
        {
            Controller.DrainMana(amount);
        }

        public IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems()
        {
            return Controller.GetAttackEffectsFromEquippedItems();
        }

        public bool LoadPlayer()
        {
            return false;
        }
    }
}
