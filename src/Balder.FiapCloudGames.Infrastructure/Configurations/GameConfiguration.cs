using Balder.FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balder.FiapCloudGames.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(g => g.Platform)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(g => g.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(g => g.Price)
                .IsRequired()
                .HasColumnType("decimal(8,2)");
        }
    }
}