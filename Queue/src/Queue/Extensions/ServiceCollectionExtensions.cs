using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Queue.Events;
using Queue.RabbitMq;
using RabbitMQ.Client;

namespace Queue.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqEventBus(this IServiceCollection services)
            => services.AddRabbitMqEventBus(() => new RabbitMqOptions());

        public static IServiceCollection AddRabbitMqEventBus(this IServiceCollection services,
            Func<RabbitMqOptions> optionsProvider)
        {
            var options = optionsProvider.Invoke();
            if (options == null) throw new ArgumentException("Options isn't provided");

            services.AddSingleton<IConnectionFactory>(sp =>
                new ConnectionFactory
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password
                });

            services.AddSingleton<IPersistentConnection, RabbitMqConnection>();
            services.AddSingleton<IEventBus, RabbitMqEventBus>(sp =>
            {
                var rabbitMqPersistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<RabbitMqEventBus>>();
                return new RabbitMqEventBus(rabbitMqPersistentConnection, logger, sp, options.QueueName, options.ExchangeName);
            });

            return services;
        }
    }
}
