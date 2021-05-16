using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Klinger.Configuration
{
    public static class LoggerConfig 
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "7e2297308ffd4d4c888c3a1e84861371";
                o.LogId = new Guid("903fc044-a523-4732-afe6-9a21ed4d8e75");
            });

            services.AddLogging(builder =>
            {
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = "7e2297308ffd4d4c888c3a1e84861371";
                    o.LogId = new Guid("903fc044-a523-4732-afe6-9a21ed4d8e75");
                });
                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Critical);
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            return app;
        }
    }
}
