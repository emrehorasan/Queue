using System.Threading.Tasks;

namespace Queue.Events
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : EventBase
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}
