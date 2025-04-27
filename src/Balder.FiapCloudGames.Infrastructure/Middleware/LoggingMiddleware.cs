using Balder.FiapCloudGames.Infrastructure.CorrelationId;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Balder.FiapCloudGames.Infrastructure.Middleware;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, ICorrelationIdGenerator correlationId)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            logger.LogInformation($"[CorrelationId: {correlationId.Get()}] Incoming request: {context.Request.Method} {context.Request.Path}");

            await next(context); 
            stopwatch.Stop();
            logger.LogInformation($"[CorrelationId: {correlationId.Get()}] Response: {context.Response.StatusCode} - Completed in {stopwatch.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.LogError(ex, $"[CorrelationId: {correlationId.Get()}] An error occurred while processing the request: {context.Request.Method} {context.Request.Path}");
            throw;
        }
    }
}