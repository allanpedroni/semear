using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Semear.Anuncios.Domain.Services;
using Semear.Anuncios.WebApi.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Semear.Anuncios.WebApi.Controllers
{

    [ApiVersion("1.0")]
    public class AnunciosController : ApiController
    {
        public readonly IAnunciosService anunciosService;

        private readonly ILogger logger;

        public AnunciosController(ILoggerFactory loggerFactory,
            IAnunciosService anunciosService)
        {
            logger = loggerFactory?.CreateLogger<AnunciosController>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
            this.anunciosService = anunciosService ??
                throw new ArgumentNullException(nameof(anunciosService));
        }

        /// <summary>
        /// Obtem os anuncios por coordenadas
        /// </summary>
        /// <param name="coordenadasUsuarioGet"></param>
        /// <returns></returns>
        [HttpGet("Coordenadas"), AllowAnonymous]
        [ProducesResponseType(typeof(AnuncioGetResponse), 200)]
        public async Task<ActionResult> ObterAnunciosPorCoordenadaDeUsuarioAsync(
            [FromQuery] CoordenadasUsuarioGet coordenadasUsuarioGet)
        {
            if (coordenadasUsuarioGet is { Latitude: 0, Longitude: 0 })
            {
                return NotFound("Latitude e longitude inexistentes.");
            }

            var models = await anunciosService.ObterPorCoordenadaAsync(
                coordenadasUsuarioGet.Latitude, coordenadasUsuarioGet.Longitude);

            if (models == null)
                return NoContent();

            var viewModels = models?
                .Select(x => x.ToViewModel(HttpContext.Request));

            return Ok(viewModels);
        }

        /// <summary>
        /// Cadastra um novo acesso para o anuncio
        /// </summary>
        /// <param name="id">Identificador do anuncio</param>
        /// <remarks>Essa informação é retornada através da rota que obtem os anuncios por coordenadas</remarks>
        /// <returns>Sucesso</returns>
        [HttpPost("{id}/Acesso"), AllowAnonymous]
        [ProducesResponseType(typeof(OkResult), 200)]
        public async Task<ActionResult> NovoAcessoAsync(
            int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            await anunciosService.NovoAcessoAsync(id);

            return Ok();
        }

        /// <summary>
        /// Realiza o download da imagem do anuncio
        /// </summary>
        /// <param name="id">Identificador do anuncio</param>
        /// <returns>Arquivo imagem do anuncio</returns>
        [HttpGet("{id}/Imagem"), AllowAnonymous]
        [ProducesResponseType(typeof(AnuncioGetResponse), 200)]
        public ActionResult DownloadImagemAsync(
            int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var imagemAnuncio = anunciosService.DownloadImagemAsync(id);

            if (imagemAnuncio == null)
            {
                return NotFound();
            }

            return File(imagemAnuncio, "image/jpeg");
        }

        /// <summary>
        /// Realiza a inclusão de uma novo anuncio
        /// </summary>
        /// <param name="texto">Texto que descreve o anuncio</param>
        /// <param name="latitude">Latitude do anuncio</param>
        /// <param name="longitude">Longitude do anuncio</param>
        /// <param name="imagem">Imagem do anuncio</param>
        /// <returns>Identificador do anuncio</returns>
        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(AnuncioPostResponse), 200)]
        public async Task<ActionResult> NovoAnuncioAsync(
            string texto, double latitude, double longitude, IFormFile imagem)
        {
            if (texto is null)
            {
                throw new ArgumentNullException(nameof(texto));
            }

            if (imagem is null)
            {
                throw new ArgumentNullException(nameof(imagem));
            }

            if (ExtensaoDoArquivoValida(imagem) == false)
            {
                throw new FormatException("Formato inválido. Apenas são aceitos a extensão jpeg.");
            }

            var idAnuncioNovo = await anunciosService.NovoAsync(texto, latitude, longitude, imagem.OpenReadStream());

            return Ok(new AnuncioPostResponse { Id = idAnuncioNovo });

            bool ExtensaoDoArquivoValida(IFormFile imagem)
            {
                if (imagem?.Length != 0)
                {
                    var nomeArquivo = Path.GetFileName(imagem.FileName);

                    return nomeArquivo.EndsWith("jpeg", StringComparison.InvariantCultureIgnoreCase) ||
                        nomeArquivo.EndsWith("jpg", StringComparison.InvariantCultureIgnoreCase);
                }

                return false;
            }
        }
    }
}
