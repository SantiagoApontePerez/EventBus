using EventBus.EventBus.Utility;
using UnityEngine;

namespace EventBus.EventBus.Runtime.Events
{
    [CreateAssetMenu(
        fileName = "StringEvent.asset",
        menuName = InspectorMenus.EventMenu + "String Event")]
    public sealed class StringEvent : Event<string>
    {
    }
}