using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    public enum GlobalGameState
    {
        Playing,
        Meny
    }

    public class GlobalState
    {
        public static GlobalGameState CurrentState = GlobalGameState.Playing;

        public static bool IsPlaying()
        {
            return CurrentState == GlobalGameState.Playing;
        }
    }
}
