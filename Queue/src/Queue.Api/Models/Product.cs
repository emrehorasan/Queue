using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Queue.Entities;

namespace Queue.Api.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }
}
