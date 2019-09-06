using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LogServiceColletionExtesions
    {
        public static IServiceCollection AddMonteOlimpoLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c =>
            {
                c.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                c.AddSerilog();
            });

            return services;
        }
    }
}