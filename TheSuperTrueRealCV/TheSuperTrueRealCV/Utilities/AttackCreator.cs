using Microsoft.Xna.Framework;

namespace TheSuperTrueRealCV.Utilities
{
    public static class AttackCreator
    {
        public static Attack CreateTestAttack(Vector2 piPosition, Vector2 piSize)
        {
            var attack = new Attack(piPosition, piSize, Vector2.Zero, ObjectManager.player, 1000);

            attack.HitBoxes.Add(new Rectangle(0, 0, 100, 100));

            return attack;
        }
    }
}
