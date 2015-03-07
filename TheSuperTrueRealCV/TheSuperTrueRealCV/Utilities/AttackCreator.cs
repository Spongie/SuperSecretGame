using CV_clone;
using Microsoft.Xna.Framework;

namespace TheSuperTrueRealCV.Utilities
{
    public static class AttackCreator
    {
        public static Attack CreateTestAttack(Vector2 piPosition, Vector2 piSize, Moving_Entity owner)
        {
            var attack = new Attack(piPosition, piSize, Vector2.Zero, owner, 1000, 500);

            attack.HitBoxes.Add(new Rectangle(0, 0, 100, 100));
            attack.Scaling.Damage = 1;

            return attack;
        }

        public static Attack CreateSkeletonAttack(Vector2 piPosition, Moving_Entity owner, Vector2 speed)
        {
            var attack = new Attack(piPosition, new Vector2(50,50), speed, owner, int.MaxValue, 500);

            attack.HitBoxes.Add(new Rectangle(0, 0, 100, 100));
            attack.Scaling.Damage = 1;
            attack.AttackType = Enums.AttackType.Moving;
            attack.ApplyGravity = true;


            return attack;
        }
    }
}
