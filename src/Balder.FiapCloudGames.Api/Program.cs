using Balder.FiapCloudGames.Api.Configurations;
using Balder.FiapCloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddServicesConfiguration();

builder.Services.AddDbContext<ApplicationDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Balder API - Fiap Game",
        Version = "v1",
        Description = "API criada para projeto fazer a gestão da plataforma de Games FIAP"
    });
});
var app = builder.Build();
app.AddAppConfiguration();
app.Run();


