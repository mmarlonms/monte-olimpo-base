using MonteOlimpo.Base.ExceptionHandler;
using MonteOlimpo.Base.ExceptionHandler.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionServiceCollectionExtensions
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IExceptionHandler, ExceptionHandler>();

            return services;
        }
    }
}