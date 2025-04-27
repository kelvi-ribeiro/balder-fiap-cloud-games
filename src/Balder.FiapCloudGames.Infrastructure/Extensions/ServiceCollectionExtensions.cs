using Balder.FiapCloudGames.Infrastructure.CorrelationId;
using Microsoft.Extensions.DependencyInjection;

namespace Balder.FiapCloudGames.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
    {

        return services;
    }
}
