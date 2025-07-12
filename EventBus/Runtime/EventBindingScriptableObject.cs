using UnityEngine;
using UnityEngine.Events;

namespace Systems.EventBus.Runtime
{
    public abstract class EventBindingScriptableObject<TEvent, TPayload> : ScriptableObject
        where TEvent : EventBase<TPayload>
        where TPayload : unmanaged
    {
        private EventBinding<TEvent> _runtimeBinding;
        
        #region Exposed Fields

        [Tooltip("The event asset to listen to.")]
        [SerializeField] private TEvent @event;

        [Tooltip("Response with the event payload")]
        [SerializeField] private UnityEvent<TPayload> onEvent;

        [Tooltip("Response without parameters when the event is raised.")]
        [SerializeField] private UnityEvent onEventNoParams;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            RegisterEvent();
        }

        private void OnDisable()
        {
            DeregisterEvent();
        }
        
        #endregion
        
        #region Private

        private void RegisterEvent()
        {
            _runtimeBinding = new EventBinding<TEvent>(HandleEvent);
            EventBus<TEvent>.Register(_runtimeBinding);
        }

        private void DeregisterEvent()
        {
            EventBus<TEvent>.Deregister(_runtimeBinding);
        }

        private void HandleEvent(TEvent raisedEvent)
        {
            if (raisedEvent != @event) return;
            
            onEvent?.Invoke(raisedEvent.GetValue);
            onEventNoParams?.Invoke();
        }
        
        #endregion
        
        #region API

        public void AddListener(UnityAction<TPayload> listener)
        {
            onEvent.AddListener(listener);
        }

        public void AddListener(UnityAction listener)
        {
            onEventNoParams.AddListener(listener);
        }

        public void RemoveListener(UnityAction<TPayload> listener)
        {
            onEvent.RemoveListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            onEventNoParams.RemoveListener(listener);
        }
        
        #endregion
    }
}