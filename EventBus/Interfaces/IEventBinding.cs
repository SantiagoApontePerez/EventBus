using System;

namespace Systems.EventBus.Interfaces
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoParams { get; set; }
    }
}