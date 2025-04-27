using Balder.FiapCloudGames.Api.Settings;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Services;
using Balder.FiapCloudGames.Domain.Repositories;
using Balder.FiapCloudGames.Infrastructure.CorrelationId;
using Balder.FiapCloudGames.Infrastructure.Extensions;
using Balder.FiapCloudGames.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace Balder.FiapCloudGames.Api.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IGameService, GameService>();
        services.AddTransient<IGameRepository, GameRepository>();

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IAuthenticationSettings>(sp => sp.GetRequiredService<IOptions<AuthenticationSettings>>().Value);
        services.AddCorrelationIdGenerator();
        services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        return services;
    }
}
