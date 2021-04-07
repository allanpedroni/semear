using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Semear.Usuarios.Domain.Models;
using Semear.Usuarios.Domain.Services;
using Semear.Usuarios.WebApi.Controllers;
using Semear.Usuarios.WebApi.Dtos;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Semear.Usuarios.UnitTests
{
    public class UsuariosControllerTest
    {
        private readonly Mock<ILoggerFactory> logger;
        private readonly Mock<IUsuariosService> usuariosServiceMock;

        public UsuariosControllerTest()
        {
            logger = new Mock<ILoggerFactory>();
            usuariosServiceMock = new Mock<IUsuariosService>();
        }

        [Fact]
        public async Task NovoUsuarioAsync_ComUsuarioVazio_RetornaArgumentNullException()
        {
            //arrange

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            Func<Task> act = async () =>
            {
                await usuario.NovoUsuarioAsync(null);
            };

            //assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task NovoUsuarioAsync_ComUsuarioValido_RetornaIdUsuario()
        {
            //arrange
            usuariosServiceMock.Setup(s => s.NovoAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(1);

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            var retorno = await usuario.NovoUsuarioAsync(
                new UsuarioPost()
                {
                    Email = "novo@gmail.com",
                    Nome = "nome",
                    Senha = "123456"
                }
            );

            //assert
            var result = retorno as OkObjectResult;
            var usuarioResposta = result.Value as UsuarioPostResponse;
            usuarioResposta.Id.Should().Be(1);
        }

        [Fact]
        public async Task NovoAcessoAsync_ComEmailVazio_RetornaArgumentNullException()
        {
            //arrange

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            Func<Task> act = async () =>
            {
                await usuario.NovoAcessoAsync(null);
            };

            //assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task NovoAcessoAsync_ComEmailValido_RetornaSucesso()
        {
            //arrange
            usuariosServiceMock.Setup(s => s.NovoAcessoAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            var retorno = await usuario.NovoAcessoAsync("teste@gmail.com");

            //assert
            retorno.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task ObterUsuarioAsync_ComEmailVazio_RetornaArgumentNullException()
        {
            //arrange

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            Func<Task> act = async () =>
            {
                await usuario.ObterUsuarioAsync(null);
            };

            //assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ObterUsuarioAsync_ComEmailInvalido_RetornaNotFound()
        {
            //arrange
            usuariosServiceMock.Setup(s => s.ObterUsuarioAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario)null);

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            var retorno =
                await usuario.ObterUsuarioAsync("invalido@....");

            //assert
            retorno.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ObterUsuarioAsync_ComEmailValido_RetornaDadosUsuario()
        {
            //arrange
            usuariosServiceMock.Setup(s => s.ObterUsuarioAsync(It.IsAny<string>()))
                .ReturnsAsync(new Usuario
                {
                    Id = 1,
                    Email = "testando@gmail.com",
                    Ativo = true,
                    Nome = "teste",
                    Senha = "123456"
                });

            //act
            var usuario = new UsuariosController(logger.Object,
                usuariosServiceMock.Object);

            var retorno =
                await usuario.ObterUsuarioAsync("testando@gmail.com");

            //assert
            var result = retorno as OkObjectResult;
            var usuarioResposta = result.Value as UsuarioGetResponse;

            var esperado = new Usuario
            {
                Id = 1,
                Email = "testando@gmail.com",
                Ativo = true,
                Nome = "teste",
                Senha = "123456"
            };

            usuarioResposta.Should().BeEquivalentTo(
                esperado,
                options => options.ComparingByMembers<UsuarioGetResponse>()
            );
        }
    }
}
