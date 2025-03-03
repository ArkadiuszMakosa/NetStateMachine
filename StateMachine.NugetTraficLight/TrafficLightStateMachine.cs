using StateMachine.NugetTraficLightTraficLight;

namespace StateMachine.NugetTraficLight
{
    public class TrafficLightStateMachine : ITrafficLightStateMachine
    {
        private readonly StateMachine<TrafficLightState, TrafficLightTrigger> _trafficLightStateMachine;

        public TrafficLightStateMachine(StateMachine<TrafficLightState, TrafficLightTrigger> trafficLightStateMachine)
        {
            _trafficLightStateMachine = trafficLightStateMachine;
        }

        public TrafficLightStateMachine()
        {
            _trafficLightStateMachine = new StateMachine<TrafficLightState, TrafficLightTrigger>(); //This should not be needed with IoC
        }

        public void ConfigureStateMachine()
        {
            // |RED| -TimerElapsed-> |GREEN| -TimerElapsed-> |YELLOW|  -TimerElapsed-> |RED|
            _trafficLightStateMachine.AddTransition(TrafficLightState.Red, TrafficLightTrigger.TimerElapsed, TrafficLightState.Green);
            _trafficLightStateMachine.AddTransition(TrafficLightState.Green, TrafficLightTrigger.TimerElapsed, TrafficLightState.Yellow);
            _trafficLightStateMachine.AddTransition(TrafficLightState.Yellow, TrafficLightTrigger.TimerElapsed, TrafficLightState.Red);

            // |RED| -EmergencyStop-> |RED|
            _trafficLightStateMachine.AddTransition(TrafficLightState.Red, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);
            // |Yellow| -EmergencyStop-> |RED|
            _trafficLightStateMachine.AddTransition(TrafficLightState.Yellow, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);
            // |Green| -EmergencyStop-> |RED|
            _trafficLightStateMachine.AddTransition(TrafficLightState.Green, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);
        }

        public TrafficLightState Fire(TrafficLightTrigger tTrafficLightTrigger)
        {
            return _trafficLightStateMachine.Fire(tTrafficLightTrigger);
        }

        public void SetInitialState(TrafficLightState initialState)
        {
            ConfigureStateMachine();
            _trafficLightStateMachine.SetInitialState(initialState);
        }

        public bool CanFire(TrafficLightTrigger trigger)
        {
            return _trafficLightStateMachine.CanFire(trigger);
        }

        public TrafficLightState CurrentState { get => _trafficLightStateMachine.CurrentState; }
    }
}
