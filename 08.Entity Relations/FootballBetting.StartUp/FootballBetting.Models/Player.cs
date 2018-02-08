﻿using System.Collections.Generic;

namespace FootballBetting.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public int SquadNumber { get; set; }

        public Team Team { get; set; }
        public int TeamId { get; set; }

        public Position Position { get; set; }
        public int PositionId { get; set; }

        public bool IsInjured { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();
    }
}
