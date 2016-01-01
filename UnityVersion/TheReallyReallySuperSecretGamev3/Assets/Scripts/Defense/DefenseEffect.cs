using Assets.Scripts.Character.Stats;
using System;

namespace Assets.Scripts.Defense
{
    [Serializable]
    public class DefenseEffect
    {
        public string Name = "None";
        public float Power = 0.0f;
        public float Duration = 0.0f;
        public int Ticks = 0;
        public CStats Stats;
    }
}
