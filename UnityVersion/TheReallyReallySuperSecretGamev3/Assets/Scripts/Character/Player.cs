using Assets.Scripts.Attacks;
using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Items;
using Assets.Scripts.Spells;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Defense;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(EntityStats))]
    [RequireComponent(typeof(BuffContainer))]
    public class Player : MonoBehaviour, IPlayer
    {
        public GameObject MenuController;
        public int InventoryItems;
        private EntityStats ivStats;

        public PlayerController Controller { get; private set; }

        void Start()
        {
            ivStats = GetComponent<EntityStats>();
            Controller = new PlayerController(GetComponent<BuffContainer>(), ivStats.stats, GetComponent<SpellManager>());
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
            if (Input.GetKeyDown(KeyCode.L))
                ivStats.stats.DealDamage(20f);
            if (Input.GetKeyDown(KeyCode.I))
                ivStats.stats.RewardExperience(50);

            var trueStats = Controller.GetTrueStats();
            ivStats.stats.Resources.Update(trueStats.MaximumHealth, trueStats.MaximumMana);
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

        public List<GameObject> GetAvailableSpells()
        {
            return Controller.GetAvailableSpells();
        }

        public bool CanCastSpell(SpellSlot piSpellSlot)
        {
            return Controller.CanCastSpell(piSpellSlot);
        }

        public IEnumerable<DefenseEffect> GetDefenseEffectsFromEquippedItems()
        {
            return Controller.GetDefenseEffectsFromEquippedItems();
        }
    }
}
