using Queue.Events;

namespace Queue.Consumer.ElasticSearch.Products.Events
{
    public class ProductAddedEvent : EventBase
    {
        public Product Product { get; set; }
    }
}
