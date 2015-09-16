namespace WinTail
{
    using System;
    using Akka.Actor;

    /// <summary>
    ///     Actor responsible for reading FROM the console.
    ///     Also responsible for calling <see cref="ActorSystem.Shutdown" />.
    /// </summary>
    internal class ConsoleReaderActor : UntypedActor
    {
        public const string StartCommand = "Start";
        public const string ExitCommand = "exit";

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }

            GetAndValidateInput();
        }


        // in ConsoleReadorActor, after OnReceive()
        private void DoPrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk./n");
        }

        /// <summary>
        ///     Reads input from console, validates it, then signlas appropriate response
        ///     (continue processing, error, success, etc.)
        /// </summary>
        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) &&
                string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // if user typed ExitCommand, shut down the entire actor system (allows the process to exit)
                Context.System.Shutdown();
                return;
            }

            // otherwise, just hand message off for validation
            Context.ActorSelection("akka://MyActorSystem/user/validationActor").Tell(message);
        }
    }
}