namespace CVCommon.Utility
{
    public enum Direction
    {
        Left,
        Right
    }

    public enum GameState
    {
        MainMenu,
        Settings,
        GameOver
    }

    public enum InGameState
    {
        Cutscene,
        Menu,
        ActionMode
    }

    public enum PlatformStatus
    {
        Normal,
        Ice,
        Mud
    }

    public enum PlatformType
    {
        CastleWall,
        CastleFloor,
        SlopeUp,
        SlopeDown
    }

    public enum DamageType
    {
        Magical,
        Physical,
        Super
    }

    public enum AIState
    {
        TurnAround,
        NotAktive,
        Idle,
        Attack1,
        Attack2,
        GoForward,
        GoBack,
        RunLeft,
        RunRight,
        DiveRight,
        DiveLeft,
        FlyLeft,
        FlyRight,
        Jump,
        JumpRight,
        JumpLeft,
        Spell1,
        Spell2
    }

    public enum AIResult
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Jump,
        Attack,
        SpellAttack,
        Block
    }

    public enum MonsterType
    {
        Bat,
        FuckingFrog,
        Skeleton,
        SkeletonArmor,
        SkeletonMusician,
        Ogre,
        RockOgre,
        Imp,
        GiantWarGolem,
        ElementalAdept,
        Waterelemental,
        Windelemental,
        HighMage,
        GrandMagus,
        Boss
    }

    public enum ItemType
    {
        Gem,
        MajorGem,
        Head,
        Armor,
        Cape,
        Accessories,
        Consumable
    }
}
