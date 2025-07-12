using Systems.EventBus.Runtime;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Events
{
    [CreateAssetMenu(
        fileName = "BoolBusEvent.asset",
        menuName = InspectorMenus.EventMenu + "Bool Event")]
    public sealed class BoolBusEvent : EventBase<bool> { }
}