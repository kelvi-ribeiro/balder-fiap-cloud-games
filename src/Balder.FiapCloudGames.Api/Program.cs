using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Services;
using Balder.FiapCloudGames.Domain.Repositories;
using Balder.FiapCloudGames.Infrastructure.Context;
using Balder.FiapCloudGames.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IGameRepository, GameRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(
    opt=>opt.UseSqlServer(
        "Server=localhost,1433;Database=FiapGames;User Id=sa;Password=Arthur123!;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
