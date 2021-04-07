using Semear.Usuarios.Application;
using Semear.Usuarios.Domain.Services;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            ApplicationConfiguration applicationConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (applicationConfiguration == null)
            {
                throw new ArgumentNullException(nameof(applicationConfiguration));
            }

            services.AddSingleton(applicationConfiguration);

            services.AddScoped<IUsuariosService, UsuariosService>();

            return services;
        }
    }
}
