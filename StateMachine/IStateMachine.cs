namespace StateMachine
{
    internal interface IStateMachine<TState, TTrigger> : ICustomStateMachine<TState, TTrigger>
        where TState : notnull
        where TTrigger : notnull
    {
        void AddTransition(TState aCV_PREP, TTrigger userRequestAcvRelease, TState aCV_REL_BY_USER);
    }
}
