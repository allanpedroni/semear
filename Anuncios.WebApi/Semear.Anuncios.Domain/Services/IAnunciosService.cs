using Semear.Anuncios.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Semear.Anuncios.Domain.Services
{
    public interface IAnunciosService
    {
        Task<IEnumerable<Anuncio>> ObterPorCoordenadaAsync(double latitude, double longitude);
        Stream DownloadImagemAsync(int idAnuncio);
        Task NovoAcessoAsync(int idAnuncio);
        Task<int> NovoAsync(string texto, double latitude, double longitude, Stream stream);
    }
}