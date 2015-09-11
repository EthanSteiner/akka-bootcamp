namespace WinTail
{
    using System;
    using System.Linq.Expressions;
    using Akka.Actor;

    #region Program

    internal class Program
    {
        public static ActorSystem MyActorSystem;

        private static void Main(string[] args)
        {
            // initialize MyActorSystem
            MyActorSystem = ActorSystem.Create("Bob");

            Expression<Func<ConsoleWriterActor>> expression = () => new ConsoleWriterActor();
            var props = Props.Create(expression);
            var consoleWriterActor = MyActorSystem.ActorOf(props);
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriterActor)));

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.AwaitTermination();
        }
    }

    #endregion
}