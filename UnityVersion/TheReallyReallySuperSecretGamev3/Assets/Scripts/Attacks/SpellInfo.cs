using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class SpellInfo : MonoBehaviour
    {
        public string Description;
        //public AttackDamageScaling attackScaling;

        public override string ToString()
        {
            return Description;
        }
    }
}
