using Queue.Events;

namespace Queue.Consumer.Mongo.Products.Events
{
    public class ProductAddedEvent : EventBase
    {
        public Product Product { get; set; }
    }
}
