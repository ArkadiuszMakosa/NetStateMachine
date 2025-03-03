using StateMachine;

/// <summary>
/// Represents a generic state machine that manages state transitions based on triggers.
/// </summary>
/// <typeparam name="TState">The type representing the states in the state machine.</typeparam>
/// <typeparam name="TTrigger">The type representing the triggers that cause state transitions.</typeparam>
/// <remarks>
/// This class implements a simple state machine where transitions between states are triggered by specific events.
/// Each state can have multiple transitions defined, each associated with a unique trigger.
/// The state machine must be initialized with an initial state before any operations can be performed.
/// </remarks>
/// <example>
/// <code>
/// var stateMachine = new StateMachine<string, int>();
/// stateMachine.SetInitialState("Start");
/// stateMachine.AddTransition("Start", 1, "End");
/// stateMachine.Fire(1); // Transitions from "Start" to "End"
/// </code>
/// </example>
public class StateMachine<TState, TTrigger> : IStateMachine<TState, TTrigger>
    where TState : notnull
    where TTrigger : notnull
{
    private readonly Dictionary<TState, Dictionary<TTrigger, TState>> _transitions;
    private TState _currentState;
    private bool isInitialStateSeated = false;

    // Creates an empty state machine with no defined transitions.
    /// The state machine uses a nested dictionary to store state transitions,
    /// where the outer dictionary maps states to their possible triggers,
    /// and the inner dictionary maps triggers to resulting states.
    public StateMachine()
    {
        _transitions = new Dictionary<TState, Dictionary<TTrigger, TState>>();
    }

    /// Gets the current state of the state machine.
    /// <value>The current state of type <typeparamref name="TState"/>.</value>    public TState CurrentState => _currentState;
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