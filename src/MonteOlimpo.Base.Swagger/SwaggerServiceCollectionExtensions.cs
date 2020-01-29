using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MonteOlimpo.Base.Crosscutting.Swagger;
using MonteOlimpo.Base.Extensions.Configuration;
using MonteOlimpo.Base.Filters.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        private static readonly string xmlCommentsFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

        public static IServiceCollection AddMonteOlimpoSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            var apiMetaData = configuration.GetSection(nameof(SwaggerConfiguration)).TryGet<SwaggerConfiguration>();

            if (apiMetaData is null)
                throw new ArgumentException(nameof(apiMetaData));

            if (string.IsNullOrWhiteSpace(xmlCommentsFilePath))
                throw new ArgumentException(nameof(xmlCommentsFilePath));

            services.AddVersionedApiExplorer(
               options =>
               {
                   options.GroupNameFormat = "'v'VVV";
                   options.SubstituteApiVersionInUrl = true;
               });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Parse(apiMetaData.DefaultVersion);
            });

            services.AddSwaggerGen(
                    options =>
                    {
                        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        });

                        var apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Where(a => !a.IsDeprecated)
                        .OrderBy(a => a.ApiVersion.MajorVersion).ThenBy(a => a.ApiVersion.MinorVersion))
                            options.SwaggerDoc(description.GroupName, CreateSwaggerInfoForApiVersion(description, apiMetaData));

                        options.SchemaFilter<SwaggerExcludeFilter>();
                        options.OperationFilter<SwaggerDefaultValues>();
                        options.IncludeXmlComments(xmlCommentsFilePath);
                    });

            return services;
        }

        private static OpenApiInfo CreateSwaggerInfoForApiVersion(ApiVersionDescription description, SwaggerConfiguration  swaggerConfiguration)
        {
            var info = new OpenApiInfo()
            {
                Title = swaggerConfiguration.Name,
                Description = swaggerConfiguration.Description,
                Version = description.ApiVersion.ToString()
            };

            if (info.Version.Equals("1.0"))
                info.Description += "<br>Initial version.";
            else
            {
                var versionDescription = swaggerConfiguration.VersionIngDescriptions?[info.Version];
                if (!string.IsNullOrWhiteSpace(versionDescription))
                    info.Description += $"<br>{versionDescription}";
            }

            if (description.IsDeprecated)
                info.Description += "<br><br><span style=\"color: #ff0000;font-weight: bold;\">This version is already deprecated.</span>";

            return info;
        }
    }
}