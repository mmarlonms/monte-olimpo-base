using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MonteOlimpo.Base.Authentication;
using System;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfiguration jwtConfiguration = configuration.TryGet<JwtConfiguration>();
            services.AddSingleton(jwtConfiguration ?? throw new ArgumentNullException(nameof(JwtConfiguration)));

            var validIssuer = Guid.TryParse(jwtConfiguration.Issuer, out var guidIssuer)
                ? guidIssuer.ToString()
                : jwtConfiguration.Issuer;

            var validAudience = Guid.TryParse(jwtConfiguration.Audience, out var guidAudience)
                ? guidAudience.ToString()
                : jwtConfiguration.Audience;

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.RequireHttpsMetadata = false;
                    bearerOptions.SaveToken = true;
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = !string.IsNullOrWhiteSpace(validAudience),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = validIssuer,
                        ValidAudience = validAudience,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
                    };
                });

            return services;
        }
    }
}
