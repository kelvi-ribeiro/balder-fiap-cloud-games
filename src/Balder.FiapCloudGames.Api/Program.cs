using Balder.FiapCloudGames.Api.Configurations;
using Balder.FiapCloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddServicesConfiguration();

builder.Services.AddDbContext<ApplicationDbContext>(
    opt=>opt.UseSqlServer(
        "Server=127.0.0.1,1433;Database=FiapGames;User Id=sa;Password=FiapGames!;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true"));

var app = builder.Build();
app.AddAppConfiguration();
app.Run();
