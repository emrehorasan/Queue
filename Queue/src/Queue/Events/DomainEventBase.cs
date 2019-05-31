using System;
using System.Collections.Generic;

namespace Queue.Events
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public string Type => GetType().Name;
        public DateTime CreationTime { get; }
        public Dictionary<string, object> Args { get; }
        public DomainEventBase()
        {
            CreationTime = DateTime.Now;
            Args = new Dictionary<string, object>();
        }
        public abstract void Flatten();
    }
}
