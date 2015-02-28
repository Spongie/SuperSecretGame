using CV_clone;
using CV_clone.Utilities;
using CVCommon;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TheSuperTrueRealCV.Utilities;

namespace TheSuperTrueRealCV
{
    public enum AttackTarget
    {
        Player,
        Monsters,
        Everything
    }

    public enum AttackType
    {
        FollowOwner,
        Moving,
        Stationary
    }

    public class Attack : Moving_Entity
    {
        private Vector2 positionOffset;

        public Attack() : base(ContentHolder.tmp, Vector2.Zero, Vector2.Zero)
        {
            ApplyGravity = false;
            positionOffset = Owner.WorldPosition - WorldPosition;
        }

        public Dictionary<Moving_Entity, Timer> EntitiesHit { get; set; }

        public List<Rectangle> HitBoxes { get; set; }
        public List<Timer> HitboxTimers { get; set; }

        public Moving_Entity Owner { get; set; }
        public AttackType AttackType { get; set; }
        public AttackDamageScaling Scaling { get; set; }

        public Rectangle CurrentHitbox
        {
            get { return HitBoxes.First(); }
        }

        public Rectangle RealHitbox
        {
            get { return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, CurrentHitbox.Width, CurrentHitbox.Height); }
        }

        public AttackTarget TargetType { get; set; }

        public bool CanHitEntity(Moving_Entity piEntity)
        {
            if (!EntitiesHit.ContainsKey(piEntity))
                return true;

            return EntitiesHit[piEntity].Done;
        }

        public override void Update(GameTime time)
        {
            if (AttackType == TheSuperTrueRealCV.AttackType.FollowOwner)
                WorldPosition = Owner.WorldPosition + positionOffset;
            else if (AttackType == TheSuperTrueRealCV.AttackType.Stationary)
                Speed = Vector2.Zero;

            HandleHitboxChanging(time);

            base.Update(time);
        }

        private void HandleHitboxChanging(GameTime time)
        {
            if (HitboxTimers.Count > 0)
            {
                HitboxTimers.First().Update(time);

                if (HitboxTimers.First().Done)
                {
                    HitboxTimers.Remove(HitboxTimers.First());
                    HitBoxes.Remove(CurrentHitbox);
                }
            }
        }
    }
}
