using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMonteOlimpoSwagger(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint($"/swagger/v1/swagger.json","Api");
                });

            return applicationBuilder;
        }
    }
}