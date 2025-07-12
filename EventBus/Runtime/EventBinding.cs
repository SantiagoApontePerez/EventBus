using System;
using Systems.EventBus.Interfaces;

namespace Systems.EventBus.Runtime
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventNoParams = () => { };
        
        Action<T> IEventBinding<T>.OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }
        Action IEventBinding<T>.OnEventNoParams
        {
            get => _onEventNoParams;
            set => _onEventNoParams = value;
        }

        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        public EventBinding(Action onEventNoParams) => _onEventNoParams = onEventNoParams;

        public void Add(Action<T> onEvent) => _onEvent += onEvent;
        public void Add(Action onEvent) => _onEventNoParams += onEvent;

        public void Remove(Action<T> onEvent) => _onEvent -= onEvent;
        public void Remove(Action onEvent) => _onEventNoParams -= onEvent;
    }
}