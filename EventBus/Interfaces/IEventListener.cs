namespace Systems.EventBus.Interfaces
{
    public interface IEventListener<T>
    {
        void OnEventRaised(T value);
    }
}