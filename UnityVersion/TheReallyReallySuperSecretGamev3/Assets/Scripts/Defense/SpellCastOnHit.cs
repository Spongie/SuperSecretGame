using Assets.Scripts.Attacks.Modifier;
using Assets.Scripts.Attacks;
using UnityEngine;

namespace Assets.Scripts.Defense
{
    public class SpellCastOnHit : Modifier
    {
        public GameObject Spell;

        public override float ApplyEffect(GameObject piAttacker, GameObject piDefender, AttackDamageScaling piAttackScaling, Vector3 piHitpoint, float piCurrentDamage)
        {
            GameObject explosion = (GameObject)Instantiate(Spell, piHitpoint, Quaternion.identity);
            var attackOfSpell = explosion.GetComponent<Attack>();
            attackOfSpell.Owner = piDefender;
            explosion.SetActive(true);

            return piCurrentDamage;
        }
    }
}
