using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Queue.Consumer.Mongo.Products.Events;
using Queue.Events;

namespace Queue.Consumer.Mongo.Products.Extensions
{
    public static class ProviderExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddTransient<ProductAddedEventHandler>();
            return services;
        }

        public static IApplicationBuilder UseConsumers(this IApplicationBuilder applicationBuilder)
        {
            var eventBus =  applicationBuilder.ApplicationServices.GetService<IEventBus>();
            eventBus.Subscribe<ProductAddedEvent, ProductAddedEventHandler>();
            return applicationBuilder;
        }
    }
}
