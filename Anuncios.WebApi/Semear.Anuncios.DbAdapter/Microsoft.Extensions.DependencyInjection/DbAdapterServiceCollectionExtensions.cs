using Semear.Anuncios.DbAdapter;
using Semear.Anuncios.Domain.Adapters;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DbAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddAnunciosDbAdapter(this IServiceCollection services,
            DbAdapterConfiguration dbAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (dbAdapterConfiguration == null)
            {
                throw new ArgumentNullException(nameof(dbAdapterConfiguration));
            }

            // Registra a instancia do objeto de configuracoes desta camanda.
            services.AddSingleton(dbAdapterConfiguration);

            services.AddScoped<IDbConnection>(d =>
            {
                return new SqlConnection(dbAdapterConfiguration.SqlConnectionString);
            });

            services.AddScoped<IAnunciosAdapter, AnunciosAdapter>();

            return services;
        }
    }
}
