using System;
using RabbitMQ.Client;

namespace Queue
{
    public interface IPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
