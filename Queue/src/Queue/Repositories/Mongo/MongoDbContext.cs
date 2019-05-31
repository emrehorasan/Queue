using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Queue.Repositories.Mongo
{
    public class MongoDbContext
    {
        private readonly string _dbName;
        private readonly MongoClient _client;

        public MongoDbContext(IConfiguration configuration)
        {
            _dbName = configuration.GetSection("Mongo:DbName").Value;
            _client = new MongoClient(configuration.GetSection("Mongo:ConnectionString").Value);
        }

        public IMongoDatabase GetDatabase() => _client.GetDatabase(_dbName);
    }
}
