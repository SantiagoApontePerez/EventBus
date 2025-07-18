using EventBus.EventBus.Interfaces;
using UnityEngine;

namespace EventBus.EventBus.Runtime
{
    public abstract class EventListener<T, TE> : MonoBehaviour, IEventListener<T> where TE : Event<T>
    {
        [SerializeField] private TE gameEvent;

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