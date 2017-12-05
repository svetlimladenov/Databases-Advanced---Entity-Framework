using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
            
        }

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-D8U60HB\SQLEXPRESS;Database=FootballBetting;Integrated Security=True");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //•	A Team has one PrimaryKitColor and one SecondaryKitColor
            //•	A Color has many PrimaryKitTeams and many SecondaryKitTeams

            modelBuilder.Entity<Color>()
                .HasMany(c => c.PrimaryKitTeams)
                .WithOne(t => t.PrimaryKitColor)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Color>()
                .HasMany(c => c.SecondaryKitTeams)
                .WithOne(t => t.SecondatyKitColor)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict); 

            //•	A Team residents in one Town
            //•	A Town can host several Teams
            modelBuilder.Entity<Town>()
                .HasMany(t => t.Teams)
                .WithOne(tm => tm.Town)
                .HasForeignKey(tm => tm.TeamId);

            //•	A Town can be placed in one Country and a Country can have many Towns
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(t => t.CountryId);

            //•	A Game has one HomeTeam and one AwayTeam and a Team can have many HomeGames and many AwayGames
            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeGames)
                .WithOne(g => g.HomeTeam)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(g => g.HomeTeamId);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayGames)
                .WithOne(g => g.AwayTeam)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(g => g.AwayTeamId);

            //•	A Player can play at one Position and one Position can be played by many Players
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Position)
                .WithMany(ps => ps.Players)
                .HasForeignKey(p => p.PositionId);
            
            //•	A Player can play for one Team and one Team can have many Players
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            //•	One Player can play in many Games and in each Game, many Players take part (PlayerStatistics)

            modelBuilder.Entity<PlayerStatistic>()
                .HasKey(pk => new {pk.PlayerId,pk.GameId});

            modelBuilder.Entity<PlayerStatistic>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId);

            modelBuilder.Entity<PlayerStatistic>()
                .HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);

            //•	A Bet can be placed by only one User and one User can place many Bets

            modelBuilder.Entity<User>()
                .HasMany(u => u.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            //•	Many Bets can be placed on one Game, but a Bet can be only on one Game
            modelBuilder.Entity<Bet>()
                .HasOne(b => b.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(b => b.GameId);

        }
    }
}
