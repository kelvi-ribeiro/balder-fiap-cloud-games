using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Balder.FiapCloudGames.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Tabelas
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
        }
    }
}