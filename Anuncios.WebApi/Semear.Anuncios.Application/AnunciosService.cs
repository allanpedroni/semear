using Microsoft.Extensions.Logging;
using Semear.Anuncios.Domain.Adapters;
using Semear.Anuncios.Domain.Models;
using Semear.Anuncios.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Semear.Anuncios.Application
{
    public class AnunciosService : IAnunciosService
    {
        private readonly ILogger logger;
        private readonly IAnunciosAdapter anunciosAdapter;

        public AnunciosService(ILoggerFactory loggerFactory, 
            IAnunciosAdapter anunciosAdapter)
        {
            logger = loggerFactory?.CreateLogger<AnunciosService>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
            this.anunciosAdapter = anunciosAdapter ?? 
                throw new ArgumentNullException(nameof(anunciosAdapter));
        }

        public Task<int> NovoAsync(string texto, double latitude, double longitude, Stream stream)
        {
            //Pode ser feito uma validação de existencia do cliente

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var anuncioNovo = new Anuncio
                {
                    Texto = texto,
                    Latitude = latitude,
                    Longitude = longitude,
                    Imagem = Convert.ToBase64String(fileBytes)
                };

                return anunciosAdapter.NovoAsync(anuncioNovo);
            }
        }

        public Stream DownloadImagemAsync(int idAnuncio)
        {
            logger.LogInformation($"{nameof(DownloadImagemAsync)}: Obtendo anuncio por id: " +
                "{idAnuncio}.", idAnuncio);

            return anunciosAdapter.DownloadImagemAsync(idAnuncio);
        }

        public async Task<IEnumerable<Anuncio>> 
            ObterPorCoordenadaAsync(double latitude, double longitude)
        {
            logger.LogInformation($"{nameof(ObterPorCoordenadaAsync)}: Obtendo anuncio por coordenadas.");

            return await anunciosAdapter.ObterPorCoordenadaAsync(
                new Coordenada { Latitude = latitude, Longitude = longitude });
        }

        public async Task NovoAcessoAsync(int idAnuncio)
        {
            logger.LogInformation($"{nameof(NovoAcessoAsync)}: Computar acesso ao " +
                "anuncio {IdAnuncio}.", idAnuncio);

            await anunciosAdapter.NovoAcessoAsync(idAnuncio);
        }
    }
}
