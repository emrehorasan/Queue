using Queue.Entities;

namespace Queue.Consumer.ElasticSearch.Products
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }
}
