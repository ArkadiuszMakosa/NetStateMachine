using StateMachine;

public class StateMachine<TState, TTrigger> : IStateMachine<TState, TTrigger>
{
    private readonly Dictionary<TState, Dictionary<TTrigger, TState>> _transitions;
    private TState _currentState;
    private bool isInitialStateSeated = false;

    public StateMachine()
    {
        _transitions = new Dictionary<TState, Dictionary<TTrigger, TState>>();
    }

    public TState CurrentState => _currentState;

    public void SetInitialState(TState initialState)
    {
        if (isInitialStateSeated)
        {
            throw new InvalidOperationException("Cannot run SetInitialState twice.");
        }

        isInitialStateSeated = true;
        _currentState = initialState;
    }

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
        CheckIfIsInitialStateSet();

        return _transitions.ContainsKey(_currentState) && _transitions[_currentState].ContainsKey(trigger);
    }

    public TState Fire(TTrigger trigger)
    {
        CheckIfIsInitialStateSet();

        if (!CanFire(trigger))
        {
            var errorMessage = $"Cannot transition from state {_currentState} with trigger {trigger}";

            throw new InvalidOperationException(errorMessage);
        }

        var targetState = _transitions[_currentState][trigger];
        _currentState = targetState;

        return _currentState;
    }

    private void CheckIfIsInitialStateSet()
    {
        if (!isInitialStateSeated)
        {
            throw new InvalidOperationException("Initial state is not set.");
        }
    }
}