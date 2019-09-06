using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MonteOlimpo.Extensions.Configuration;

namespace MonteOlimpo.Sample.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigurationExtensions.AddMonteOlimpoConfiguration)
                .UseStartup<Startup>();
    }
}
