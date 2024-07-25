public class StateMachine<TState, TTrigger>
{
    private readonly Dictionary<TState, Dictionary<TTrigger, TState>> _transitions;
    private TState _currentState;

    public StateMachine(TState initialState)
    {
        _transitions = new Dictionary<TState, Dictionary<TTrigger, TState>>();
        _currentState = initialState;
    }

    public TState CurrentState => _currentState;

    public void AddTransition(TState fromState, TTrigger trigger, TState toState)
    {
        if (!_transitions.ContainsKey(fromState))
        {
            _transitions[fromState] = new Dictionary<TTrigger, TState>();
        }

        if (_transitions[fromState].ContainsKey(trigger))
        {
            throw new InvalidOperationException($"You tried to assign transition twice: From {fromState} with trigger {trigger}");
        }

        _transitions[fromState][trigger] = toState;
    }

    public bool CanFire(TTrigger trigger)
    {
        return _transitions.ContainsKey(_currentState) && _transitions[_currentState].ContainsKey(trigger);
    }

    public TState Fire(TTrigger trigger)
    {
        if (!CanFire(trigger))
        {
            throw new InvalidOperationException($"Cannot transition from state {_currentState} with trigger {trigger}");
        }

        _currentState = _transitions[_currentState][trigger];

        return _currentState;
    }
}
