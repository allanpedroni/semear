using Dapper;
using Semear.Anuncios.Domain.Adapters;
using Semear.Anuncios.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Semear.Anuncios.DbAdapter
{
    public class AnunciosAdapter : IAnunciosAdapter
    {
        private readonly IDbConnection dbConnection;
        static AnunciosAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public AnunciosAdapter(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ??
                throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<int> NovoAsync(Anuncio anuncioNovo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Texto", anuncioNovo.Texto);
            parametros.Add("@Latitude", anuncioNovo.Latitude);
            parametros.Add("@Longitude", anuncioNovo.Longitude);
            parametros.Add("@Imagem", anuncioNovo.Imagem);
            
            var query = @"
                INSERT INTO Anuncio (Texto, Latitude, Longitude, Imagem) 
                OUTPUT Inserted.Id
                VALUES(@Texto, @Latitude, @Longitude, @Imagem);

                DECLARE @idAnuncio AS INT = SCOPE_IDENTITY()

                INSERT INTO AnuncioAcesso(Id, TotalAcesso) VALUES(@idAnuncio, 0)";

            var idAnuncio = await dbConnection.ExecuteScalarAsync<int>(query, parametros);

            return idAnuncio;
        }

        public Stream DownloadImagemAsync(int idAnuncio)
        {
            var query = "SELECT Imagem FROM ANUNCIO WHERE Id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Id", idAnuncio);

            var imagemBase64 = dbConnection.QueryFirstOrDefault<string>(query, parametros);

            var bytesImagem = Convert.FromBase64String(imagemBase64);

            return new MemoryStream(bytesImagem);
        }

        public async Task<IEnumerable<Anuncio>> ObterPorCoordenadaAsync(Coordenada coordenadaCliente)
        {
            var query =
                "SELECT a.Id, a.Texto, a.Latitude, a.Longitude, aa.TotalAcesso as TotalAcessos " +
                "FROM Anuncio a INNER JOIN AnuncioAcesso aa ON a.Id = aa.Id";

            var anuncios = await dbConnection.QueryAsync<Anuncio>(query);

            var retorno = new List<Anuncio>();

            foreach (var anuncio in anuncios)
            {
                var distanciaEmMetros = CoordenadaGeografica.ObterDistanciaEntreDoisPontosEmMetros(coordenadaCliente.Latitude, coordenadaCliente.Longitude,
                    anuncio.Latitude, anuncio.Longitude);

                if (distanciaEmMetros <= 15)
                {
                    retorno.Add(anuncio);
                }
            }

            return retorno;
        }

        public async Task NovoAcessoAsync(int idAnuncio)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@IdAnuncio", idAnuncio);

            var query = @"
                UPDATE AnuncioAcesso
                SET TotalAcesso = TotalAcesso+1
                WHERE Id=@IdAnuncio";

            await dbConnection.ExecuteAsync(query, parametros);
        }
    }

    //TODO: Ajustar documentação e lugar certo que vai ficar. VERIFICAR se deve mover para application ou adapter
    internal static class CoordenadaGeografica
    {
        /// <summary>
        /// haversine. Formula: a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)
        /// </summary>
        /// <param name="latitude1"></param>
        /// <param name="longitude1"></param>
        /// <param name="latitude2"></param>
        /// <param name="longitude2"></param>
        /// <remarks>https://www.movable-type.co.uk/scripts/latlong.html</remarks>
        /// <returns></returns>
        public static double ObterDistanciaEntreDoisPontosEmMetros(double latitude1, double longitude1,
            double latitude2, double longitude2)
        {
            var raioTerra = 6371e3;
            var lat1 = latitude1 * Math.PI / 180;
            var lat2 = latitude2 * Math.PI / 180;
            var deltaLatitude = (latitude2 - latitude1) * Math.PI / 180;
            var deltaLongitude = (longitude2 - longitude1) * Math.PI / 180;

            var haversineFormulaParte1 = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                      Math.Cos(lat1) * Math.Cos(lat2) *
                      Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
            var haversineFormulaParte2 = 
                2 * Math.Atan2(Math.Sqrt(haversineFormulaParte1), Math.Sqrt(1 - haversineFormulaParte1));

            return raioTerra * haversineFormulaParte2; // in metres
        }
    }
}
