using EventBus.EventBus.Utility;
using UnityEngine;

namespace EventBus.EventBus.Runtime.Events
{
    [CreateAssetMenu(
        fileName = "IntEvent.asset",
        menuName = InspectorMenus.EventMenu + "Int Event")]
    public sealed class IntEvent : Event<int>
    {
    }
}