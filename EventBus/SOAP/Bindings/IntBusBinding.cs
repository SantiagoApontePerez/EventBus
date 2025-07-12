using Systems.EventBus.Runtime;
using Systems.EventBus.SOAP.Events;
using Systems.EventBus.Utility;
using UnityEngine;

namespace Systems.EventBus.SOAP.Bindings
{
    [CreateAssetMenu(
        fileName = "IntBusBinding.asset", 
        menuName = InspectorMenus.BindingMenu + "Int Binding"
    )]
    public sealed class IntBusBinding : EventBindingScriptableObject<IntBusEvent, int> { }
}