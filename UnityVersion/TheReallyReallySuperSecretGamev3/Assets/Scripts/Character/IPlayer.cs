using Assets.Scripts.Attacks;
using Assets.Scripts.Character.Stat;
using Assets.Scripts.Items;
using System.Collections.Generic;

namespace Assets.Scripts.Character
{
    public interface IPlayer
    {
        void GiveLoot(Item piDrop);

        void RewardExp(int piAmount);

        CStats GetTrueStats();

        IEnumerable<AttackEffect> GetAttackEffectsFromEquippedItems();

        bool LoadPlayer();
    }
}
