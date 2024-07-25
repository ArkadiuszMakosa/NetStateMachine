
namespace StateMachine.Runner
{
    internal class TraficLightStateMachine
    {
        private readonly StateMachine<TrafficLightState, TrafficLightTrigger> trafficLight;

        public TraficLightStateMachine(TrafficLightState beginningState)
        {
            trafficLight = new(beginningState);
            ConfigureStateMachine();
        }

        public void ConfigureStateMachine()
        {
            trafficLight.AddTransition(TrafficLightState.Red, TrafficLightTrigger.TimerElapsed, TrafficLightState.Green);
            trafficLight.AddTransition(TrafficLightState.Green, TrafficLightTrigger.AnotherTimerElapsed, TrafficLightState.Yellow);



            // |RED| -TimerElapsed-> |GREEN| -TimerElapsed-> |YELLOW|  -TimerElapsed-> |RED|
            trafficLight.AddTransition(TrafficLightState.Red, TrafficLightTrigger.TimerElapsed, TrafficLightState.Green);
            trafficLight.AddTransition(TrafficLightState.Green, TrafficLightTrigger.TimerElapsed, TrafficLightState.Yellow);
            trafficLight.AddTransition(TrafficLightState.Yellow, TrafficLightTrigger.TimerElapsed, TrafficLightState.Red);

            // |RED| -EmergencyStop-> |RED|
            trafficLight.AddTransition(TrafficLightState.Red, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);
            // |Yellow| -EmergencyStop-> |RED|
            trafficLight.AddTransition(TrafficLightState.Yellow, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);
            // |Green| -EmergencyStop-> |RED|
            trafficLight.AddTransition(TrafficLightState.Green, TrafficLightTrigger.EmergencyStop, TrafficLightState.Red);

            //trafficLight.AddTransition(TrafficLightState.Green, TrafficLightTrigger.TestTrigger, TrafficLightState.Green);
        }

        internal TrafficLightState Fire(TrafficLightTrigger tTrafficLightTrigger)
        {
            return trafficLight.Fire(tTrafficLightTrigger);
        }

        public TrafficLightState CurrentState { get => trafficLight.CurrentState; }
    }
}
