using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Balder.FiapCloudGames.Infrastructure.Context;
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

        modelBuilder.Entity<User>().HasData(
            new User("a64eaba9-8753-466a-9500-7ac3ada50342", "Admin", "admin@admin.com", "$2a$11$qMAkSQCPQ/AgL3JqFv/aI.TNxO2FRFs8rWjzx1c2Zm6PWwupVrHXi", "admin"), //Password - adminFG123!
            new User("a7155384-ed2e-47a8-b603-60a5aa9ba424", "User", "user@fiapgames.com", "$2a$11$dACyarlQmDHImkKicCYvtem86VRXQfv9SgI7XMH/Ol.P0ducjIeB2", "user")  //Password - userFG123!
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}