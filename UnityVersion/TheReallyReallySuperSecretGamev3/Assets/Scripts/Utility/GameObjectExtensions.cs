using Assets.Scripts.Character;
using Assets.Scripts.Character.Monsters;
using Assets.Scripts.Character.Stats;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class GameObjectExtensions
    {
        public static CStats GetGameObjectsStats(this GameObject piObject)
        {
            Player player = piObject.GetComponent<Player>();

            if (player != null)
                return player.GetTrueStats();

            return piObject.GetComponent<Monster>().GetTrueStats();
        }
    }
}
