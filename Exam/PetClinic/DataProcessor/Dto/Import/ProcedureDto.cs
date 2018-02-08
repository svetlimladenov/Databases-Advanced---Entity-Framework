using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using PetClinic.Models;

namespace PetClinic.DataProcessor.Dto.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [XmlAttribute("Vet")]
        public string Vet { get; set; }

        [Required]
        [XmlAttribute("Animal")]
        public string Animal { get; set; }

        [Required]
        [XmlAttribute("DateTime")]
        public DateTime DateTime { get; set; }

        [XmlElement("AnimalAids")]
        public AnimalAidsProcedureDto[] AnimalAidsProcedureDto { get; set; } = new AnimalAidsProcedureDto[0];

    }
    [XmlType("AnimalAid")]
    public class AnimalAidsProcedureDto
    {
        [Required]
        [XmlAttribute("Name")]        
        public string Name { get; set; }
    }


}
