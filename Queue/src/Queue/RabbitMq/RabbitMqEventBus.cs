using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Queue.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Queue.RabbitMq
{
    public class RabbitMqEventBus : IEventBus
    {
        #region .ctor

        private readonly IPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMqEventBus> _logger;
        private readonly IServiceProvider _provider;
        private string _queueName;
        private readonly string _exchangeName;
        private IModel _channel;

        public RabbitMqEventBus(IPersistentConnection persistentConnection, ILogger<RabbitMqEventBus> logger, IServiceProvider provider, string queueName, string exchangeName)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _provider = provider;
            _queueName = queueName;
            _exchangeName = exchangeName;
        }

        #endregion

        public void Publish(EventBase @event)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var model = JsonConvert.SerializeObject(@event);
            using (var channel = _persistentConnection.CreateModel())
            {
                channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(model);
                channel.BasicPublish(_exchangeName,
                    routingKey: @event.GetType().Name,
                    basicProperties: null,
                    body: body);
                _logger.LogDebug("[x] Sent {0}", model);
            }
        }

        public void Subscribe<TEvent, TEventHandler>() where TEvent : EventBase where TEventHandler : IEventHandler<TEvent>
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            if(_channel == null)
                Initialization();

            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: typeof(TEvent).Name);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var handler = _provider.GetRequiredService<TEventHandler>();
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonConvert.DeserializeObject<TEvent>(message);
                handler.Handle(@event);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        private void Initialization()
        {
            _channel = _persistentConnection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
        }
    }
}
