using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonteOlimpo.ApiBoot;
using MonteOlimpo.Base.Core.Data.UnitOfWork;
using MonteOlimpo.Base.Core.Domain.UnitOfWork;
using MonteOlimpo.Repository;
using MonteOlimpo.Sample.Data;
using System.Collections.Generic;
using System.Reflection;

namespace MonteOlimpo.Sample.WebApi
{
    public class Startup : MonteOlimpoBootStrap
    {
        public Startup(IConfiguration configuration)
            :base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<SampleContext>(
                   options => options.UseNpgsql(
                       Configuration.GetConnectionString(nameof(SampleContext))));

            services.AddScoped<IUnitOfWork, UnitOfWork<SampleContext>>();

        }

        protected override IEnumerable<Assembly> GetAditionalAssemblies()
        {
            yield return typeof(DeusRepository).Assembly;
        }
    }
}