using Systems.EventBus.Interfaces;
using UnityEngine;

namespace Systems.EventBus.Runtime
{
    /// <summary>
    /// Base class for all events. Responsible for holding and setting the event value.
    /// </summary>
    /// <typeparam name="T">Any unmanaged value type.</typeparam>
    public abstract class EventBase<T> : ScriptableObject, IEvent where T : unmanaged
    {
        private T _value;

        #region API

        public T GetValue => _value;
        public void SetValue(T value) => _value = value;

        #endregion
    }
}