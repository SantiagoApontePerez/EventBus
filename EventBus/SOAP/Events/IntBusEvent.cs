using Systems.EventBus.Runtime;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Events
{
    [CreateAssetMenu(
        fileName = "IntBusEvent.asset",
        menuName = InspectorMenus.EventMenu + "Int Event")]
    public sealed class IntBusEvent : EventBase<int> { }
}