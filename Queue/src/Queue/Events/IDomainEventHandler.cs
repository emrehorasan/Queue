namespace Queue.Events
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : class, IDomainEvent, new()
    {
        void Handle(TDomainEvent args);
    } 
}
