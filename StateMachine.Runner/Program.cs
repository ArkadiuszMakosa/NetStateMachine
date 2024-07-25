namespace StateMachine.Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var valueFromDatabase = TrafficLightState.Red;

            var myStateMachine = new TraficLightStateMachine(valueFromDatabase);

            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Red

            myStateMachine.Fire(TrafficLightTrigger.TimerElapsed);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Green

            myStateMachine.Fire(TrafficLightTrigger.TimerElapsed);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Yellow

            myStateMachine.Fire(TrafficLightTrigger.EmergencyStop);
            Console.WriteLine($"Current state: {myStateMachine.CurrentState}"); // Red

            //Not Allowed transition
            valueFromDatabase = myStateMachine.Fire(TrafficLightTrigger.TestTrigger);
        }
    }
}
