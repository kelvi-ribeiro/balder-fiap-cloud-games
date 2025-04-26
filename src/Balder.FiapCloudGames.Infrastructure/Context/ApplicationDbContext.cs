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
        //TODO: Criar o método OnConfiguring para configurar o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=FiapGames;User Id=sa;Password=FiapGames!;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }
}