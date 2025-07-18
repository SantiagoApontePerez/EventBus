using System;
using System.Collections.Generic;
using EventBus.EventBus.Interfaces;
using UnityEngine;

namespace EventBus.EventBus.Runtime
{
    public abstract class Event<T> : ScriptableObject
    {
        // Tracks listeners and their registration time
        private readonly Dictionary<IEventListener<T>, DateTime> _lastInvokeTimes = new();
        
        // Tracks last invoke time per listener
        private readonly Dictionary<IEventListener<T>, DateTime> _registrationTimes = new();

        /// <summary>
        /// Number of active listeners.
        /// </summary>
        public int ListenerCount => _registrationTimes.Count;

        /// <summary>
        /// Mapping of listeners to their registration times.
        /// </summary>
        public IReadOnlyDictionary<IEventListener<T>, DateTime> RegistrationTimes => _registrationTimes;
        
        /// <summary>
        /// Mapping of listeners to their last invoke times.
        /// </summary>
        public IReadOnlyDictionary<IEventListener<T>, DateTime> LastInvokeTimes => _lastInvokeTimes;

        /// <summary>
        /// Raises the event with the specified value and recording invoke times.
        /// </summary>
        public void Raise(T value)
        {
            var now = DateTime.Now;
            foreach (var listener in _registrationTimes.Keys)
            {
                //Invokes all listeners and adds a timestamp.
                listener.OnEventRaised(value);
                _lastInvokeTimes[listener] = now;
            }
        }

        /// <summary>
        /// Registers a listener and stores its registration time.
        /// </summary>
        public void RegisterListener(IEventListener<T> listener)
        {
            //Registers the listener and adds a timestamp.
            if (!_registrationTimes.ContainsKey(listener)) _registrationTimes[listener] = DateTime.Now;
        }

        /// <summary>
        /// Unregisters a listener, removing its records.
        /// </summary>
        public void UnregisterListener(IEventListener<T> listener)
        {
            if (!_registrationTimes.Remove(listener)) return;
            _lastInvokeTimes.Remove(listener);
        }
    }
}