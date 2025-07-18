using System;
using System.Collections.Generic;
using Systems.EventBus.Interfaces;
using UnityEngine;

namespace EventBus.EventBus.Runtime
{
    /// <summary>
    /// Base class for all events. Responsible for holding and setting the event value.
    /// </summary>
    /// <typeparam name="T">Any unmanaged value type.</typeparam>
    public abstract class Event<T> : ScriptableObject
    {
        private readonly Dictionary<IEventListener<T>, DateTime> _registrationTimes = new();
        private readonly Dictionary<IEventListener<T>, DateTime> _lastInvokeTimes = new();

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

        public void RegisterListener(IEventListener<T> listener)
        {
            //Registers the listener and adds a timestamp.
            if(!_registrationTimes.ContainsKey(listener)) _registrationTimes[listener] = DateTime.Now;
        }

        public void UnregisterListener(IEventListener<T> listener)
        {
            if (!_registrationTimes.ContainsKey(listener)) return;
            
            _registrationTimes.Remove(listener);
            _lastInvokeTimes.Remove(listener);
        }
        
        public int ListenerCount => _registrationTimes.Count;
        
        public IReadOnlyDictionary<IEventListener<T>, DateTime> RegistrationTimes => _registrationTimes;
        public IReadOnlyDictionary<IEventListener<T>, DateTime> LastInvokeTimes => _lastInvokeTimes;
    }
}