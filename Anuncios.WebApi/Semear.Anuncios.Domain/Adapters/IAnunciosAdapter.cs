using Semear.Anuncios.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Semear.Anuncios.Domain.Adapters
{
    public interface IAnunciosAdapter
    {
        Task<IEnumerable<Anuncio>> ObterPorCoordenadaAsync(Coordenada coordenada);
        Stream DownloadImagemAsync(int idAnuncio);
        Task NovoAcessoAsync(int idAnuncio);
        
        Task<int> NovoAsync(Anuncio anuncioNovo);
    }
}