using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto.Impot
{
    public class SeatingClassDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)] //-- "Tochno 2 simvola"
        public string Abbreviation { get; set; }
    }
}
