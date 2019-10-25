using MonteOlimpo.Base.ValidationHandler;
using MonteOlimpo.Base.ValidationHandler.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationHandling(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IValidationHandler, ValidationHandler>();

            return services;
        }
    }
}