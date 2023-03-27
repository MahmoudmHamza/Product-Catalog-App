using System;
using System.Collections.Generic;

namespace ProductsCatalogApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
} 