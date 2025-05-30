﻿using Balder.FiapCloudGames.Infrastructure.Extensions;

namespace Balder.FiapCloudGames.Api.Configurations;
public static class AppConfiguration
{
    public static WebApplication AddAppConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseLoggingMiddleware();
        app.UseCorrelationMiddleware();
        return app;
    }
}
