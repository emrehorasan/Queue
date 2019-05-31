using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.Api.Repositories;
using Queue.Api.Services.Products;
using Queue.Extensions;
using Queue.RabbitMq;
using Queue.Repositories.Mongo;
using Swashbuckle.AspNetCore.Swagger;

namespace Queue.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<ElasticDbContext>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ProductMongoRepository>();
            services.AddTransient<ProductElasticRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
