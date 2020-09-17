using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonteOlimpo.Base.Extensions.Service;
using MonteOlimpo.Base.Filters.Exceptions;
using MonteOlimpo.Base.Filters.Validation;
using System.Collections.Generic;
using System.Reflection;


namespace MonteOlimpo.Base.ApiBoot
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
                options.Filters.Add<ValidatorActionFilter>();
                options.EnableEndpointRouting = false;
            })
          .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
          .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblies(this.GetValidationAssemblies()));

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddExceptionHandling();
            services.AddValidationHandling();
            services.AddMonteOlimpoSwagger(Configuration);
            services.RegisterAllTypes(GetAditionalAssemblies());
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.Equals("Development"))
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

        protected virtual IEnumerable<Assembly> GetAditionalAssemblies()
        {
            yield return typeof(MonteOlimpoBootStrap).Assembly;
        }

        protected virtual IEnumerable<Assembly> GetValidationAssemblies()
        {
            yield return typeof(MonteOlimpoBootStrap).Assembly;
        }
    }
}