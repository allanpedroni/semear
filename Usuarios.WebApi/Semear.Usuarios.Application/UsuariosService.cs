using Microsoft.Extensions.Logging;
using Semear.Usuarios.Domain.Adapters;
using Semear.Usuarios.Domain.Models;
using Semear.Usuarios.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Semear.Usuarios.Application
{
    public class UsuariosService : IUsuariosService
    {
        private readonly ILogger logger;
        private readonly IUsuariosAdapter usuariosAdapter;

        public UsuariosService(ILoggerFactory loggerFactory,
            IUsuariosAdapter UsuariosAdapter)
        {
            logger = loggerFactory?.CreateLogger<UsuariosService>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
            this.usuariosAdapter = UsuariosAdapter ??
                throw new ArgumentNullException(nameof(UsuariosAdapter));
        }

        public async Task NovoAcessoAsync(string email)
        {
            logger.LogInformation($"{nameof(NovoAcessoAsync)}: Incluindo novo acesso do usuário.." +
                "{Email}.", email);

            await usuariosAdapter.NovoAcessoAsync(email);
        }

        public async Task<int> NovoAsync(Usuario usuario)
        {
            logger.LogInformation($"{nameof(NovoAsync)}: Incluindo um novo usuario " +
                "{@Usuario}.", usuario);

            return await usuariosAdapter.NovoAsync(usuario);
        }

        public async Task<Usuario> ObterUsuarioAsync(string email)
        {
            logger.LogInformation($"{nameof(ObterUsuarioAsync)}: Incluindo um novo usuario " +
                "{Email}.", email);

            return await usuariosAdapter.ObterUsuarioAsync(email);
        }
    }
}
