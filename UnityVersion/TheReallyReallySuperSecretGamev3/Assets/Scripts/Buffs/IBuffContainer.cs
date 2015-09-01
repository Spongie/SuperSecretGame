using Assets.Scripts.Character.Stat;

namespace Assets.Scripts.Buffs
{
    public interface IBuffContainer
    {
        void ApplyBuff(Buff piBuff);
        void ClearBuff(Buff piBuff);
        void SetStats(CStats piStats);
        CStats GetBuffStats();
        bool IsStunned();
        bool IsChilled();
        bool IsFeared();
    }
}
