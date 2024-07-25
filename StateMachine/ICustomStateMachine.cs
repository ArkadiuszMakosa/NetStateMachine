namespace StateMachine
{
    public interface ICustomStateMachine<TState, TTrigger>
    {
        void SetInitialState(TState initialState);

        bool CanFire(TTrigger trigger);

        TState Fire(TTrigger trigger);

        TState CurrentState { get; }
    }
}
