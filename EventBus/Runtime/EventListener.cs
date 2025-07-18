using EventBus.Runtime;
using Systems.EventBus.Interfaces;
using UnityEngine;

namespace EventBus.EventBus.Runtime
{
    public abstract class EventListener<T, E> : MonoBehaviour, IEventListener<T> where E : Event<T>
    {
        [SerializeField] private E gameEvent;

        private void OnEnable()
        {
            if(gameEvent != null) gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if(gameEvent != null) gameEvent.UnregisterListener(this);
        }

        public abstract void OnEventRaised(T value);
    }
}