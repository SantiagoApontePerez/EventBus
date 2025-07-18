using EventBus.Runtime;
using Systems.EventBus.Utility;
using UnityEngine;

namespace EventBus.EventBus.Runtime.Events
{
    [CreateAssetMenu(
        fileName = "FloatEvent.asset", 
        menuName = InspectorMenus.EventMenu + "Float Event")]
    public sealed class FloatEvent : Event<float> { }
}