namespace EventBus.EventBus.Interfaces
{
    public interface IEventListener<in T>
    {
        void OnEventRaised(T value);
    }
}