using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class SeatingClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(2,MinimumLength = 2)] //-- "Tochno 2 simvola"
        public string Abbreviation { get; set; }
    }
}
