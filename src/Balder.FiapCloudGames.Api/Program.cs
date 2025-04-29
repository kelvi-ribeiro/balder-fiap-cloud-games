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
    opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.AddAppConfiguration();
app.Run();
