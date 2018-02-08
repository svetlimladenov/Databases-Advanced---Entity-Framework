using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Models
{
    public class Passport
    {
        [Key]
        [RegularExpression(@"^[A-z]{7}\d{3}$")]
        public string SerialNumber { get; set; }
        
        [Required]
        public Animal Animal { get; set; }

        [Required]
        [RegularExpression(@"^(0|\+359)\d{9}$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate  { get; set; }
    }
}
