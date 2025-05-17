using Balder.FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balder.FiapCloudGames.Infrastructure.Configurations;

public class GameUserConfiguration : IEntityTypeConfiguration<GameUser>
{
    public void Configure(EntityTypeBuilder<GameUser> builder)
    {
        builder.HasKey(gu => gu.Id);

       builder.HasOne(gu => gu.Game)
            .WithMany(g => g.GameUsers) 
            .HasForeignKey(gu => gu.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(gu => gu.User)
            .WithMany(u => u.GameUsers) 
            .HasForeignKey(gu => gu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(gu => new { gu.GameId, gu.UserId })
            .IsUnique();
    }
}
