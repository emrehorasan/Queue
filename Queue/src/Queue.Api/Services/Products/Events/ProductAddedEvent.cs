using Queue.Api.Models;
using Queue.Events;

namespace Queue.Api.Services.Products.Events
{
    public class ProductAddedEvent : EventBase
    {
        public Product Product { get; set; }
    }
}
