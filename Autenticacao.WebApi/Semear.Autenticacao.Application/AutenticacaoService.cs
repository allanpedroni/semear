using Microsoft.Extensions.Logging;
using Semear.Autenticacao.Domain.Adapters;
using Semear.Autenticacao.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Semear.Autenticacao.Application
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly ILogger logger;
        private readonly ITokenAdapter tokenAdapter;

        public AutenticacaoService(ILoggerFactory loggerFactory,
            ITokenAdapter tokenAdapter)
        {
            logger = loggerFactory?.CreateLogger<AutenticacaoService>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
            this.tokenAdapter = tokenAdapter ??
                throw new ArgumentNullException(nameof(tokenAdapter));
        }

        public async Task<string> ObterToken(string email, string senha)
        {
            logger.LogInformation($"{nameof(ObterToken)}: Obtendo token..");

            //PROPOSTA V2: AQUI DEVE SER REALIZADO UMA VERIFICAÇÃO DE EXISTENCIA EMAIL/SENHA, CASO CONTRÁRIO
            //SERÁ LANÇADO UM ERRO DE NEGÓCIO A SER INTERPRETADO PELO MICROSERVIÇO

            var token = tokenAdapter.Obter(email, senha);

            logger.LogInformation($"{nameof(ObterToken)}: Incluindo novo acesso do " +
                "usuário {Email}..", email);

            return await Task.FromResult(token);
        }
    }
}
