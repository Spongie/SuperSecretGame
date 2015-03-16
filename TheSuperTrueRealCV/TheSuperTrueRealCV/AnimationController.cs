using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TheSuperTrueRealCV
{
    public class AnimationController
    {
        private Dictionary<string, Animation> ivAnimations;

        public Animation CurrentAnimation { get; private set; }

        public void Update(GameTime gameTime)
        {
            if (CurrentAnimation != null)
                CurrentAnimation.Update(gameTime);
        }

        public void AddAnimation(string piName, Animation piAnimtion)
        {
            ivAnimations.Add(piName, piAnimtion);
        }

        public void StartAnimation(string piName)
        {
            CurrentAnimation = ivAnimations[piName];
        }
    }
}
