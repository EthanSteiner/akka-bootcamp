namespace ChartApp.Actors
{
    using Akka.Actor;

    /// <summary>
    ///     Signal used to indicate that it's time to sample all counters
    /// </summary>
    public class GatherMetrics
    {
    }

    /// <summary>
    ///     Metric data at the time of sample
    /// </summary>
    public class Metric
    {
        public Metric(string series, float counterValue)
        {
            CounterValue = counterValue;
            Series = series;
        }

        public string Series { get; private set; }

        public float CounterValue { get; private set; }
    }

    /// <summary>
    ///     All types of counters supported by this example
    /// </summary>
    public enum CounterType
    {
        Cpu,
        Memory,
        Disk
    }

    /// <summary>
    ///     Enables a counter and begins publishing values to <see cref="Subscriber" />
    /// </summary>
    public class SubscribeCounter
    {
        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }

        public IActorRef Subscriber { get; private set; }

        public CounterType Counter { get; private set; }
    }

    public class UnsubscribeCounter
    {
        public UnsubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }

        public CounterType Counter { get; private set; }

        public IActorRef Subscriber { get; private set; }
    }
}