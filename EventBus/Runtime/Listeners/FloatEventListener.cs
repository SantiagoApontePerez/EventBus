using EventBus.EventBus.Runtime.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus.EventBus.Runtime.Listeners
{
    public class FloatEventListener : EventListener<float, FloatEvent>
    {
        [SerializeField] private UnityEvent<float> onEventRaised;

        public override void OnEventRaised(float value)
        {
            onEventRaised?.Invoke(value);
        }
    }
}