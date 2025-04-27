using Balder.FiapCloudGames.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Balder.FiapCloudGames.Infrastructure.Extensions;

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}