using Systems.EventBus.Interfaces;
using Systems.EventBus.Runtime;
using UnityEngine;

namespace Systems.EventBus.Extensions
{
    public static class EventExtension
    {
        public static void Raise<T>(this T eventAsset) where T : ScriptableObject, IEvent
        {
            EventBus<T>.Raise(eventAsset);
        }
    }
}