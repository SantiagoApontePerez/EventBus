using System.Diagnostics.Tracing;
using EventBus.EventBus.Runtime;
using EventBus.EventBus.Runtime.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus.Runtime.Listeners
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