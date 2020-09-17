using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using MonteOlimpo.Base.Extensions.Configuration;
using MonteOlimpo.Base.Log;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LogServiceColletionExtesions
    {
        public static IHostBuilder AddMonteOlimpoLogging(this IHostBuilder builder)
        {
            ConfigureLogging();

            builder.UseSerilog();

            return builder;
        }

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("serilogsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"serilogsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();

            var logConfiguration = configuration.TryGet<LogConfiguration>();

            if (!logConfiguration.EnableLog)
                return;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration);

            if (logConfiguration.EnableLogElasticSearch)
            {
                var elasticConfiguration = configuration.TryGet<ElasticConfiguration>();
                log.WriteTo.Elasticsearch(ConfigureElasticSink(elasticConfiguration, environment));
            }

            if (logConfiguration.EnableLogConsole)
            {
                log.WriteTo.Console();
            }

            Serilog.Log.Logger = log.CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(ElasticConfiguration elasticConfiguration, string environment)
        {
            var opt = new ElasticsearchSinkOptions(new Uri(elasticConfiguration.UrlHost))
            {
                AutoRegisterTemplate = true,
                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                IndexFormat = $"{elasticConfiguration.IndexName.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };

            if (elasticConfiguration.UseAuthentication)
            {
                opt.ModifyConnectionSettings = cfn =>
                cfn.BasicAuthentication(elasticConfiguration.User, elasticConfiguration.Password)
                .ServerCertificateValidationCallback((o, certificate, arg3, arg4) => { return true; });
            }

            return opt;
        }
    }
}
