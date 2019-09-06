using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonteOlimpo.Extensions.Service;
using MonteOlimpo.Filters.Exceptions;
using System.Collections.Generic;
using System.Reflection;

namespace MonteOlimpo.ApiBoot
{
    public abstract class MonteOlimpoBootStrap
    {
        protected MonteOlimpoBootStrap(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMonteOlimpoLogging(Configuration);
            services.AddExceptionHandling();
            services.AddMonteOlimpoSwagger(Configuration);
            services.RegisterAllTypes(GetAditionalAssemblies());
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseMonteOlimpoSwagger();
        }

        protected abstract IEnumerable<Assembly> GetAditionalAssemblies();
    }
}