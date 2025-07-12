using Systems.EventBus.Runtime;
using Systems.EventBus.SOAP.Events;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Bindings
{
    [CreateAssetMenu(
        fileName = "FloatBusBinding.asset", 
        menuName = InspectorMenus.BindingMenu + "Float Binding"
    )]
    public sealed class FloatBusBinding : EventBindingScriptableObject<FloatBusEvent, float> { }
}