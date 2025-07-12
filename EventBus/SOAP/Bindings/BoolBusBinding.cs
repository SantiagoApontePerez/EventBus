using Systems.EventBus.Runtime;
using Systems.EventBus.SOAP.Events;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Bindings
{
    [CreateAssetMenu(
        fileName = "BoolBusBinding.asset", 
        menuName = InspectorMenus.BindingMenu + "Bool Binding"
    )]
    public sealed class BoolBusBinding : EventBindingScriptableObject<BoolBusEvent, bool> { }
}