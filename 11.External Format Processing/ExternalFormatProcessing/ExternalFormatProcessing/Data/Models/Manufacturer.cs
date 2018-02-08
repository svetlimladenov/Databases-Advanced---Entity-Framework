﻿using System.Collections.Generic;

namespace ExternalFormatProcessing.Data.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } //= new List<Product>();
    }
}
