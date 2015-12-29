using Assets.Scripts.Attacks;
using Assets.Scripts.Character.Stats;
using Assets.Scripts.Items;
using Assets.Scripts.Spells;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public interface IPlayer
    {
        void GiveLoot(Item piDrop);

        void RewardExp(int piAmount);

        CStats GetTrueStats();

        IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems();
        List<GameObject> GetAvailableSpells();
        bool LoadPlayer();
        bool CanCastSpell(SpellSlot piSpellSlot);
    }
}
