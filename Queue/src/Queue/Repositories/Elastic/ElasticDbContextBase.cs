using System;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Queue.Repositories.Elastic
{
    public abstract class ElasticDbContextBase
    {
        public ElasticClient Client { get; }
        public string IndexName { get; }

        protected ElasticDbContextBase(IConfiguration configuration)
        {
            IndexName = configuration.GetSection("Elastic:Index").Value;
            var settings = new ConnectionSettings(new Uri(configuration.GetSection("Elastic:ConnectionString").Value))
                .DefaultIndex(IndexName);

            Client  = new ElasticClient(OnConfigure(settings));
        }

        protected abstract ConnectionSettings OnConfigure(ConnectionSettings settings);
    }
}
