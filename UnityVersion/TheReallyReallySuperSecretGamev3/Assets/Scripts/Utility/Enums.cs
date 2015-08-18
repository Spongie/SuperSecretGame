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
