using System;
using System.Collections.Generic;

namespace Queue.Events
{
    public interface IDomainEvent
    {
        string Type { get; }
        DateTime CreationTime { get; }
        Dictionary<string, object> Args { get; }
    }
}
