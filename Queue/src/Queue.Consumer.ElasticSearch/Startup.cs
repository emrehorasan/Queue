using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.Consumer.ElasticSearch.Products.Extensions;
using Queue.Extensions;
using Queue.RabbitMq;
using Queue.Repositories;
using Queue.Repositories.Elastic;

namespace Queue.Consumer.ElasticSearch
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ElasticDbContext>()
                    .AddSingleton<ElasticDbContextBase, ElasticDbContext>()
                    .AddSingleton(typeof(IRepository<>), typeof(ElasticRepository<>));

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsumers();
            app.Run(async context => await context.Response.WriteAsync("Hello ElasticSearch Consumer!"));
        }
    }
}
