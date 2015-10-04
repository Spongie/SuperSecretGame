using UnityEngine;

namespace Assets.Scripts.Character
{
    public class HashIDs : MonoBehaviour
    {
        public int Running;
        public int Idle;
        public int Falling;
        public int BoostLand;
        public int Dash;
        public int DiveKick;
        public int Stab;
        public int Smash;
        public int Jump;
        public int BoostJump;
        public int LandCancel;
        public int BoostEnd;
        public int SpellSelfCast;
        public int SpellOffensiveCast;

        void Awake()
        {
            Running = Animator.StringToHash("Running");
            Idle = Animator.StringToHash("Idle");
            Falling = Animator.StringToHash("Falling");
            BoostLand = Animator.StringToHash("BoostLand");
            Dash = Animator.StringToHash("Dash");
            DiveKick = Animator.StringToHash("DiveKick");
            Stab = Animator.StringToHash("Stab");
            Smash = Animator.StringToHash("Smash");
            Jump = Animator.StringToHash("Jump");
            BoostJump = Animator.StringToHash("BoostJump");
            LandCancel = Animator.StringToHash("LandCancel");
            BoostEnd = Animator.StringToHash("BoostEnd");
            SpellSelfCast = Animator.StringToHash("SpellSelfCast");
            SpellOffensiveCast = Animator.StringToHash("SpellOffensiveCast");
        }
    }
}
