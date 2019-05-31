using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Queue.EventBus;
using Queue.EventBus.Impl;
using RabbitMQ.Client;

namespace Queue.RabbitMq.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services)
            => services.AddEventBusRabbitMQ(() => new RabbitMQOptions());

        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services,
            Func<RabbitMQOptions> optionsProvider)
        {
            var options = optionsProvider.Invoke();
            if (options == null) throw new ArgumentException("Options isn't provided");

            services.AddSingleton<IConnectionFactory>(sp =>
                new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password
                });

            services.AddSingleton<IPersistentConnection, DefaultPersistentConnection>();
            services.AddSingleton<ISubscriptionsManager, InMemorySubscriptionsManager>();

            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                var rabbitMqPersistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<ISubscriptionsManager>();
                return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, options.QueueName, options.ExchangeName, options.RetryCount);
            });

            return services;
        }
    }
}
