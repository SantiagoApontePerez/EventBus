using EventBus.EventBus.Utility;
using UnityEngine;

namespace EventBus.EventBus.Runtime.Events
{
    [CreateAssetMenu(
        fileName = "BoolEvent.asset",
        menuName = InspectorMenus.EventMenu + "Bool Event")]
    public sealed class BoolEvent : Event<bool>
    {
    }
}