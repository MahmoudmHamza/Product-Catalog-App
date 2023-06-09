﻿using System;
using System.Collections.Generic;

namespace ProductsCatalogApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
    }
}