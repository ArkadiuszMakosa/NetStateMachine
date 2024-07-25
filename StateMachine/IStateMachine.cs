namespace StateMachine
{
    internal interface IStateMachine<TState, TTrigger> : ICustomStateMachine<TState, TTrigger>
    {
        void AddTransition(TState aCV_PREP, TTrigger userRequestAcvRelease, TState aCV_REL_BY_USER);
    }
}
