using System.Collections.Generic;

namespace ExternalFormatProcessing.Data.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
    }
}
