using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MonteOlimpo.Base.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void AddMonteOlimpoConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var configuration = builder.Build();

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("serilogsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile($"serilogsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Try to deserialize a configuration section into a predefined settings class; throws an exception if it fails.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static T TryGet<T>(this IConfiguration configuration) where T : class
        {
            var configName = typeof(T).Name;
            if (configuration.GetChildren().Any(config => config.Key == configName))
                configuration = configuration.GetSection(configName);

            var model = configuration.Get<T>() ??
                throw new InvalidOperationException($"Configuration section '{typeof(T).Name}' for type '{typeof(T).FullName}' is missing.");

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                throw new InvalidOperationException($"One or more attributes are missing from the configuration section '{typeof(T).Name}' for type '{typeof(T).FullName}'. " +
                    $"---> {string.Join(" | ", validationResults.Select(vr => vr.ErrorMessage))}");

            return model;
        }
    }
}