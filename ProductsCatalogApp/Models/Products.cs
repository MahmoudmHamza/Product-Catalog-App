﻿using System;
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
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<ProductRating> Ratings { get; set; }
        public decimal Rating { get; set; }
    }
}