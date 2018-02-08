﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto.Impot
{
    public class TrainDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        public string Type { get; set; } = "HighSpeed";

        public SeatDto[] Seats { get; set; } = new SeatDto[0];
    }
}
