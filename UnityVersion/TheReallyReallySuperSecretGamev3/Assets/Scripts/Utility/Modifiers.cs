using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Modifiers
    {
        protected GameObject FireBall;
        protected GameObject ArcaneExplosion;

        public Modifiers()
        {
            FireBall = (GameObject)Resources.Load("SpellPrefabs/Fireball", typeof(GameObject));
            ArcaneExplosion = (GameObject)Resources.Load("Arcane Explosion/Fireball", typeof(GameObject));
        }
    }
}
