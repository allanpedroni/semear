using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Semear.Autenticacao.TokenAdapter;
using Semear.Autenticacao.Domain.Adapters;
using System;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TokenAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddTokenAdapter(this IServiceCollection services,
            JwtConfiguration jwtConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (jwtConfiguration == null)
            {
                throw new ArgumentNullException(nameof(jwtConfiguration));
            }

            services.AddSingleton(jwtConfiguration);

            services.AddScoped<ITokenAdapter, TokenAdapter>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Secret)),
                };
            });

            return services;
        }
    }
}
