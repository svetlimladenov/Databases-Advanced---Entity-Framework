using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data
{
    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(e => e.Initials)
                .HasColumnType("NCHAR(3)")
                .IsRequired(true);

            //•	A Team has one PrimaryKitColor and one SecondaryKitColor
            //•	A Color has many PrimaryKitTeams and many SecondaryKitTeams
            builder.HasOne(t => t.PrimaryKitColor)
                .WithMany(pk => pk.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.SecondaryKitColor)
                .WithMany(sk => sk.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            //•	A Team residents in one Town
            //•	A Town can host several Teams
            builder.HasOne(t => t.Town)
                .WithMany(tn => tn.Teams)
                .HasForeignKey(t => t.TownId);
        }
    }
}