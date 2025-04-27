
using Balder.FiapCloudGames.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Balder.FiapCloudGames.Infrastructure.Extensions;
public static class CorrelationMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationMiddleware>();
    }
}