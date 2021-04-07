using Semear.Anuncios.DbAdapter;
using Semear.Usuarios.Domain.Adapters;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DbAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddAnunciosDbAdapter(this IServiceCollection services)
        //DbAdapterConfiguration DbAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //if (DbAdapterConfiguration == null)
            //{
            //    throw new ArgumentNullException(nameof(DbAdapterConfiguration));
            //}

            //// Registra a instancia do objeto de configuracoes desta camanda.
            //services.AddSingleton(DbAdapterConfiguration);

            //services.AddScoped<IDbConnection>(d =>
            //{
            //    return new SqlConnection(DbAdapterConfiguration.SqlConnectionString);
            //});

            services.AddScoped<IAnunciosAdapter, AnunciosAdapter>();

            return services;
        }
    }
}
