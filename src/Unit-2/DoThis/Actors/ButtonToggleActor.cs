﻿namespace ChartApp.Actors
{
    using System.Windows.Forms;
    using Akka.Actor;

    /// <summary>
    ///     Actor responsible for managing button toggles
    /// </summary>
    internal class ButtonToggleActor : UntypedActor
    {
        private readonly IActorRef _coordinatorActor;
        private readonly Button _myButton;

        private readonly CounterType _myCounterType;
        private bool _isToggledOn;

        public ButtonToggleActor(IActorRef coordinatorActor, Button myButton, CounterType myCounterType, bool isToggledOn = false)
        {
            _coordinatorActor = coordinatorActor;
            _myButton = myButton;
            _isToggledOn = isToggledOn;
            _myCounterType = myCounterType;
        }

        protected override void OnReceive(object message)
        {
            if (message is Toggle && _isToggledOn)
            {
                // toggle is currently on

                // stop watching this counter
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Unwatch(_myCounterType));

                FlipToggle();
            }

            else if (message is Toggle &&
                     !_isToggledOn)
            {
                // toggle is currently off

                //start watching this counter
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Watch(_myCounterType));

                FlipToggle();
            }

            else
            {
                Unhandled(message);
            }
        }

        private void FlipToggle()
        {
            // flip the toggle
            _isToggledOn = !_isToggledOn;

            // change the text of the button
            _myButton.Text = string.Format(
                                           "{0} ({1})",
                                           _myCounterType.ToString().ToUpperInvariant(),
                                           _isToggledOn ? "ON" : "OFF");
        }

        /// <summary>
        ///     Toggles this button on or off and sends the appropriate messages
        /// </summary>
        public class Toggle
        {
        }
    }
}