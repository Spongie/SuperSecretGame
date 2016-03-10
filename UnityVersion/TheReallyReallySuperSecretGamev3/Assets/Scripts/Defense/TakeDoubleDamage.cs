using Assets.Scripts.Attacks.Modifier;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    public class TakeDoubleDamage : Modifier
    {
        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            return piCurrentDamage * 2f;
        }
    }
}
