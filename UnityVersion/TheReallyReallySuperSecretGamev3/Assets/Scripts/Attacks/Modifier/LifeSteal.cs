using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/Lifesteal")]
    public class LifeSteal : Modifier
    {
        [Tooltip("% of damage thats converted to life")]
        public float Amount;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            DamageController.DealDamageToGameObject(piAttacker, -(int)(piCurrentDamage * Amount));

            return piCurrentDamage;
        }
    }
}
