using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Semear.Autenticacao.Domain.Services;
using Semear.Autenticacao.WebApi.Dtos;
using System;
using System.Threading.Tasks;

namespace Semear.Autenticacao.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class AutenticacaoController : ApiController
    {
        private readonly ILogger logger;
        private readonly IAutenticacaoService autenticacaoService;

        public AutenticacaoController(ILoggerFactory loggerFactory,
            IAutenticacaoService autenticacaoService)
        {
            logger = loggerFactory?.CreateLogger<AutenticacaoController>() ??
                throw new ArgumentNullException(nameof(loggerFactory));

            this.autenticacaoService = autenticacaoService ??
                throw new ArgumentNullException(nameof(autenticacaoService));
        }

        /// <summary>
        /// Obtem do token atual
        /// </summary>
        /// <returns>Dados do token atual</returns>
        [HttpGet, Authorize]
        [Route("current")]
        public async Task<ActionResult> QuemEstaLogadoAsync()
        {
            var tokenAtual = await HttpContext.GetTokenAsync("Bearer", "access_token");

            return Ok(new { Email = User.Identity.Name, Token = tokenAtual });
        }

        /// <summary>
        /// Obtem a autenticação token para ser trabalhada como premicia de segurança nas demais apis
        /// </summary>
        /// <param name="autenticacaoPost">Dados para obtenção do token</param>
        /// <returns>Token gerado de acordo com usuário e senha</returns>
        [HttpPost, AllowAnonymous]
        [Route("auth")]
        public async Task<ActionResult> AutenticacaoAsync(
            [FromBody] AutenticacaoPost autenticacaoPost)
        {
            if (autenticacaoPost is null)
            {
                throw new ArgumentNullException(nameof(autenticacaoPost));
            }

            // Gera o Token
            var token = await autenticacaoService.ObterToken(autenticacaoPost.Email, autenticacaoPost.Senha);

            // Retorna os dados
            return Ok(new AutenticacaoPostResponse { Token = token });
        }
    }
}
