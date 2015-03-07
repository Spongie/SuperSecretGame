using CV_clone;
using CV_clone.Utilities;
using CVCommon;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TheSuperTrueRealCV.Utilities;
using Microsoft.Xna.Framework.Graphics;
using TheSuperTrueRealCV.Utilities.Enums;

namespace TheSuperTrueRealCV
{
    public class Attack : Moving_Entity
    {
        private Vector2 positionOffset;
        private float msToLive;
        private Timer lifeTimer;
        private Vector2 leftOffset;
        private Vector2 rightOffset;

        public Attack(Vector2 piPosition, Vector2 piSize, Vector2 piSpeed, Moving_Entity piOwner, float piMsToLiveOnLast) : base(ContentHolder.tmp, piPosition, piSize)
        {
            Owner = piOwner;
            Speed = piSpeed;
            ApplyGravity = false;
            leftOffset = Owner.WorldPosition - WorldPosition;
            rightOffset = WorldPosition - new Vector2(Owner.WorldRect.Right, Owner.WorldRect.Y);
            HitBoxes = new List<Rectangle>();
            HitboxTimers = new List<Timer>();
            EntitiesHit = new Dictionary<Moving_Entity, Timer>();
            msToLive = piMsToLiveOnLast;
            Scaling = new AttackDamageScaling();
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

        public Rectangle ScreenCollision
        {
            get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, CurrentHitbox.Width, CurrentHitbox.Height); }
        }

        public bool ReadyToDestroy { get; private set; }

        public AttackTarget TargetType { get; set; }

        public bool CanHitEntity(Moving_Entity piEntity)
        {
            if (piEntity == Owner && TargetType == AttackTarget.Everything)
                return false;

            if (!EntitiesHit.ContainsKey(piEntity))
                return true;

            return EntitiesHit[piEntity].Done;
        }

        public void Flip(Direction piDirection)
        {
            if (piDirection == Direction.Right)
                positionOffset = rightOffset;
            else
                positionOffset = leftOffset;
        }

        public override void Update(GameTime time)
        {
            if (HitboxTimers.Count == 0 && lifeTimer == null)
                lifeTimer = new Timer(msToLive);

            if (AttackType == AttackType.FollowOwner)
                WorldPosition = Owner.WorldPosition + positionOffset;
            else if (AttackType == AttackType.Stationary)
                Speed = Vector2.Zero;

            HandleHitboxChanging(time);

            if (lifeTimer != null)
            {
                lifeTimer.Update(time);
                ReadyToDestroy = lifeTimer.Done;
            }

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
                    if(HitboxTimers.Count == 0)
                    {
                        lifeTimer = new Timer(msToLive);
                    }
                    else
                        HitBoxes.Remove(CurrentHitbox);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture, ScreenCollision, new Color(Color.Red, 150));
        }
    }
}
