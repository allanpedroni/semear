using Semear.Anuncios.Application;
using Semear.Anuncios.Domain.Services;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IAnunciosService, AnunciosService>();

            return services;
        }
    }
}
