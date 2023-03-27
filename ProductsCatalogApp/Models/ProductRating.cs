
using System;
using System.Collections.Generic;

namespace ProductsCatalogApp.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}