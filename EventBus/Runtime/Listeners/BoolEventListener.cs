using EventBus.EventBus.Runtime.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus.EventBus.Runtime.Listeners
{
    public class BoolEventListener : EventListener<bool, BoolEvent>
    {
        [SerializeField] private UnityEvent<bool> onEventRaised;

        public override void OnEventRaised(bool value)
        {
            onEventRaised?.Invoke(value);
        }
    }
}