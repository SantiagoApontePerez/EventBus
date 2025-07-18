using EventBus.EventBus.Runtime.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus.EventBus.Runtime.Listeners
{
    public class IntEventListener : EventListener<int, IntEvent>
    {
        [SerializeField] private UnityEvent<int> onEventRaised;

        public override void OnEventRaised(int value)
        {
            onEventRaised?.Invoke(value);
        }
    }
}