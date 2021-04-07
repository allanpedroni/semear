using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Semear.Usuarios.Domain.Services;
using Semear.Usuarios.WebApi.Dtos;
using System;
using System.Threading.Tasks;

namespace Semear.Usuarios.WebApi.Controllers
{

    [ApiVersion("1.0")]
    public class UsuariosController : ApiController
    {
        public readonly IUsuariosService usuarioService;

        private readonly ILogger logger;

        public UsuariosController(ILoggerFactory loggerFactory,
            IUsuariosService coordenadasUsuarioService)
        {
            logger = loggerFactory?.CreateLogger<UsuariosController>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
            this.usuarioService = coordenadasUsuarioService ??
                throw new ArgumentNullException(nameof(coordenadasUsuarioService));
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuarioPost">Dados do usuário</param>
        /// <returns>Id do usuário cadastrado.</returns>
        [HttpPost, Authorize]
        [ProducesResponseType(typeof(UsuarioPostResponse), 200)]
        public async Task<ActionResult> NovoUsuarioAsync(
            [FromBody] UsuarioPost usuarioPost)
        {
            if (usuarioPost is null)
            {
                throw new ArgumentNullException(nameof(usuarioPost));
            }

            var idUsuario = await usuarioService.NovoAsync(usuarioPost.ToModel());

            return Ok(new UsuarioPostResponse() { Id = idUsuario });
        }

        /// <summary>
        /// Realiza o controle da seção ativa do usuário
        /// </summary>
        /// <param name="email">Email do usuário da sessão</param>
        /// <remarks>Deve haver controle de sessão do usuário ativo no sistema (Usuario/NovoAcesso)</remarks>
        /// <returns>Sucesso - Status Code 200</returns>
        [HttpPost("Acessos/{email}"), Authorize]
        public async Task<ActionResult> NovoAcessoAsync(
            string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            await usuarioService.NovoAcessoAsync(email);

            return Ok();
        }

        /// <summary>
        /// Obtem o usuário de acordo por email
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet("{email}"), Authorize]
        [ProducesResponseType(typeof(UsuarioGetResponse), 200)]
        public async Task<ActionResult> ObterUsuarioAsync(
            string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var usuario = await usuarioService.ObterUsuarioAsync(email);

            var viewModels = usuario?.ToViewModel();

            if (viewModels == null)
                return NotFound();

            return Ok(viewModels);
        }
    }
}
