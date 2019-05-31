using Microsoft.Extensions.Configuration;
using Nest;
using Queue.Consumer.ElasticSearch.Products;
using Queue.Repositories.Elastic;

namespace Queue.Consumer.ElasticSearch
{
    public class ElasticDbContext : ElasticDbContextBase
    {
        public ElasticDbContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override ConnectionSettings OnConfigure(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Product>(x => x
                .PropertyName(p => p.Id, "id")
                .PropertyName(p=>p.Brand,"brand")
                .PropertyName(p=>p.Name, "name")
                .PropertyName(p=>p.Price,"price")
            );
            return settings;
        }
    }
}
