using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    [CreateAssetMenu(menuName = "AttackEffects/One Shot")]
    public class OneShot : Modifier
    {
        [Tooltip("Chance in % for effect to apply")]
        [Range(0f, 100f)]
        public float Chance;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            if (Random.Range(0, 100f) < Chance)
                return float.MaxValue;

            return piCurrentDamage;
        }
    }
}
