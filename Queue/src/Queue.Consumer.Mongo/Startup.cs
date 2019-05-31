using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.Consumer.Mongo.Products.Extensions;
using Queue.Extensions;
using Queue.RabbitMq;
using Queue.Repositories;
using Queue.Repositories.Mongo;

namespace Queue.Consumer.Mongo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>()
                    .AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));

            services.AddRabbitMqEventBus(() =>
            {
                var rabbitSection = Configuration.GetSection("RabbitMq");

                return new RabbitMqOptions
                {
                    UserName = rabbitSection["UserName"],
                    Password = rabbitSection["Password"],
                    HostName = rabbitSection["Host"],
                    ExchangeName = rabbitSection["ExchangeName"],
                };
            });

            services.AddEvents();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsumers();
            app.Run(async context => await context.Response.WriteAsync("Hello MongoDb Consumer!"));
        }
    }
}
