using System;
using System.Collections.Generic;

namespace ProductsCatalogApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
} 