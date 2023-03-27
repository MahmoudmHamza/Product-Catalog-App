using System;
using System.Collections.Generic;

namespace ProductsCatalogApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public decimal Rating { get; set; }
    }
}