using StateMachine.TraficLight;

namespace StateMachine.Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var initialValue = TrafficLightState.Red;

            var myStateMachine = new TrafficLightStateMachine();
            myStateMachine.SetInitialState(initialValue);

            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Red

            myStateMachine.Fire(TrafficLightTrigger.TimerElapsed);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Green

            myStateMachine.Fire(TrafficLightTrigger.TimerElapsed);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Yellow

            myStateMachine.Fire(TrafficLightTrigger.EmergencyStop);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Red

            //Not Allowed transition
            initialValue = myStateMachine.Fire(TrafficLightTrigger.TestTrigger);
        }
    }
}
