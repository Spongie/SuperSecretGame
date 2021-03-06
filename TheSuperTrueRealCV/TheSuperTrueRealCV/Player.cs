﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheSuperTrueRealCV.Utilities;
using TheSuperTrueRealCV.Interface;
using System.Xml.Serialization;
using CVCommon.Utility;

namespace CV_clone
{
    public class Player : Moving_Entity
    {
        public Vector2 extraSpeed;
        private PlayerPortrait ivPortrait;

        public Player() { }

        public Player(Texture2D tex, Vector2 vec, Vector2 size)
            :base(tex,vec,size)
        {
            jumpPower = 340;
            ivPortrait = new PlayerPortrait();
            CurrentStats.MaximumHealth = 100;
            CurrentStats.MaximumMana = 50;
            CurrentStats.CurrentMana = 50;
            CurrentStats.CurrentHealth = 100;
            CurrentStats.Damage = 1337;
            CurrentStats.MaximumExp = 125;
            CurrentStats.RewardExperience(16);
            CurrentStats.Level = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive)
                return;

            HandleInput();
            var preUpdate = CurrentDirection;
            base.Update(gameTime);

            if(!IsFalling && !IsJumping && !Controllable)
            {
                Controllable = true;
                Speed = Vector2.Zero;
            }
            if(CurrentDirection != preUpdate)
                ObjectManager.FlipAttacks(CurrentDirection);
            if(Controllable)
                Speed -= extraSpeed;

            ivPortrait.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ivPortrait.Draw(spriteBatch, CurrentStats);
            base.Draw(spriteBatch);
        }

        private void HandleInput()
        {
            if (!Controllable)
                return;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
                extraSpeed = new Vector2(-100, 0);
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
                extraSpeed = new Vector2(100, 0);
            else
                extraSpeed = Vector2.Zero;

            if (KeyMouseReader.KeyPressed(Keys.Q))
                ObjectManager.RegisterAttack(AttackCreator.CreateTestAttack(WorldPosition + new Vector2(100, 0), Settings.objectSize, this),this);

            if (CanJump && KeyMouseReader.KeyPressed(Keys.Space))
                Jump();

            extraSpeed = Movement_Restrictions.Apply(extraSpeed);

            Speed += extraSpeed;
        }

        public Vector2 GetAmountToMove(GameTime gameTime)
        {
            return WorldPosition + ((Speed + extraSpeed) * (float)gameTime.ElapsedGameTime.TotalSeconds); 
        }

        public bool CanJump
        {
            get { return !IsFalling && !IsJumping; }
        }

        public bool Controllable { get; set; }

    }
}
