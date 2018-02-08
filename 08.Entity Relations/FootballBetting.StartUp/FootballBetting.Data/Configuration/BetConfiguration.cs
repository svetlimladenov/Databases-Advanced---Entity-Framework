using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data
{
    internal class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            //•	Many Bets can be placed on one Game, but a Bet can be only on one Game
            //•	Each bet for given game must have Prediction result

            builder.HasKey(b => b.BetId);

            builder.Property(b => b.Prediction)
                .IsRequired();

            builder.HasOne(b => b.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(b => b.GameId);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.GameId);
        }
    }
}