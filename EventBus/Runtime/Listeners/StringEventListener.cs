using EventBus.EventBus.Runtime.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus.EventBus.Runtime.Listeners
{
    public class StringEventListener : EventListener<string, StringEvent>
    {
        [SerializeField] private UnityEvent<string> onEventRaised;

        public override void OnEventRaised(string value)
        {
            onEventRaised?.Invoke(value);
        }
    }
}