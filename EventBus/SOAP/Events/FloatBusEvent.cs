using Systems.EventBus.Runtime;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Events
{
    [CreateAssetMenu(
        fileName = "FloatBusEvent.asset",
        menuName = InspectorMenus.EventMenu + "Float Event")]
    public sealed class FloatBusEvent : EventBase<float> { }
}