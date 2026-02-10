namespace HoangTuDongAnh.UP.Common.Patterns.StateMachine
{
    /// <summary>
    /// Basic state (no context).
    /// </summary>
    public interface IState
    {
        void OnEnter();
        void Tick();
        void FixedTick();
        void LateTick();
        void OnExit();
    }

    /// <summary>
    /// State with typed context/owner.
    /// </summary>
    public interface IState<TContext>
    {
        void OnEnter(TContext ctx);
        void Tick(TContext ctx);
        void FixedTick(TContext ctx);
        void LateTick(TContext ctx);
        void OnExit(TContext ctx);
    }

    /// <summary>
    /// Optional: readable id/name for debugging.
    /// </summary>
    public interface IStateInfo
    {
        string StateId { get; }
    }
}