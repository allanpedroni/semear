using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Semear.Autenticacao.Domain.Services;
using Semear.Autenticacao.WebApi.Controllers;
using Semear.Autenticacao.WebApi.Dtos;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Semear.Autenticacao.UnitTests
{
    public class AutenticacaoControllerTest
    {
        private readonly Mock<ILoggerFactory> logger;
        private readonly Mock<IAutenticacaoService> autenticacaoServiceMock;

        public AutenticacaoControllerTest()
        {
            logger = new Mock<ILoggerFactory>();
            autenticacaoServiceMock = new Mock<IAutenticacaoService>();
        }

        [Fact]
        public async Task AutenticacaoAsync_SemDadoValido_RetornaNotFound()
        {
            //act
            var autenticacao = new AutenticacaoController(logger.Object,
                autenticacaoServiceMock.Object);

            Func<Task> act = async () =>
            {
                await autenticacao.AutenticacaoAsync(null);
            };

            //assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AutenticacaoAsync_ComDadoValido_RetornaToken()
        {
            //arrange
            autenticacaoServiceMock.Setup(s => s.ObterToken(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("tokengigante");

            //act
            var autenticacao = new AutenticacaoController(logger.Object,
                autenticacaoServiceMock.Object);

            var retorno =
                await autenticacao.AutenticacaoAsync(
                    new AutenticacaoPost()
                    {
                        Email = "testando@gmail.com",
                        Senha = "123456"
                    }
                );

            //assert
            var result = retorno as OkObjectResult;
            var usuarioResposta = result.Value as AutenticacaoPostResponse;

            usuarioResposta.Token.Should().Be("tokengigante");
        }
    }
}
