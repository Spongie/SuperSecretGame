using UnityEngine;

namespace Assets.Scripts.Attacks.Modifier
{
    public abstract class Modifier : ScriptableObject
    {
        public string Name;
        public string Description;

        public abstract float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage);
    }
}
