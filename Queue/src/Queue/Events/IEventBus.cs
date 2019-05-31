namespace Queue.Events
{
    public interface IEventBus
    {
        void Publish(EventBase @event);

        void Subscribe<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;
    }
}
