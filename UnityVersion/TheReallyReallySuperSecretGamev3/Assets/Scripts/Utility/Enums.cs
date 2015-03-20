using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    public enum MovingState
    {
        Left,
        Right,
        Still
    }

    public enum AttackType
    {
        FollowOwner,
        Moving,
        Stationary
    }

    public enum AttackTarget
    {
        Player,
        Monsters,
        Everything
    }
}
