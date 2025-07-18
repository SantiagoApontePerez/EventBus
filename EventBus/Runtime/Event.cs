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
        private readonly List<IEventListener<T>> _listeners = new();

        public void Raise(T value)
        {
            foreach (var listener in _listeners.ToArray())
            {
                listener.OnEventRaised(value);
            }
        }

        public void RegisterListener(IEventListener<T> listener)
        {
            if(!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(IEventListener<T> listener)
        {
            if(_listeners.Contains(listener)) _listeners.Remove(listener);
        }
        
        public int ListenerCount => _listeners.Count;
    }
}