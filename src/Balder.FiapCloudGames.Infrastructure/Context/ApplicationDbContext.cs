using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Balder.FiapCloudGames.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Tabelas
        public DbSet<User> Users { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=FiapGames;User Id=sa;Password=Arthur123!;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true");
            }
        }
    }
}