using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ExternalFormatProcessing.Data.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } //= new List<ProductWarehouse>();
    }
}
