using System;

namespace Assets.Scripts.Attacks
{
    [Serializable]
    public class AttackEffect
    {
        public string Name = "None";
        public float Power = 0.0f;
        public float Duration = 0.0f;
        public int Ticks = 0;
    }
}
