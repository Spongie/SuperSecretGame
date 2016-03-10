using Assets.Scripts.Character.Stats;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/Manasteal")]
    public class ManaSteal : Modifier
    {
        [Tooltip("% of damage thats converted to life")]
        public float Amount;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            CStats targetStats = piDefender.GetGameObjectsStats();

            int amount = (int)(piCurrentDamage * Amount);

            if (amount > targetStats.Resources.CurrentMana)
                amount = targetStats.Resources.CurrentMana;

            DamageController.DrainManaFromGameObject(piDefender, amount);
            DamageController.DrainManaFromGameObject(piAttacker, -amount);

            return piCurrentDamage;
        }
    }
}
