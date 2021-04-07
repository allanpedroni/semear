using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Semear.Anuncios.Domain.Models;
using Semear.Anuncios.Domain.Services;
using Semear.Anuncios.WebApi.Controllers;
using Semear.Anuncios.WebApi.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Semear.Anuncios.UnitTests
{
    public class AnunciosControllerTest
    {
        private readonly Mock<ILoggerFactory> logger;
        private readonly Mock<IAnunciosService> anuncioServiceMock;

        public AnunciosControllerTest()
        {
            logger = new Mock<ILoggerFactory>();
            anuncioServiceMock = new Mock<IAnunciosService>();
        }

        [Fact]
        public async Task ObterAnunciosPorCoordenadaDeUsuarioAsync_SemCoordenadasDoUsuarioComDadosVazios_RetornaAnuncios()
        {
            //arrange
            var logger = new Mock<ILoggerFactory>();

            //act
            var anuncio = new AnunciosController(logger.Object,
                anuncioServiceMock.Object);

            var coordenadas = new CoordenadasUsuarioGet() { Latitude = 0, Longitude = 0 };

            var anunciosGetResponse =
                await anuncio.ObterAnunciosPorCoordenadaDeUsuarioAsync(coordenadas);

            //assert
            var result = anunciosGetResponse as NotFoundObjectResult;
            result.Should().BeOfType<NotFoundObjectResult>();
            result.Value.Should().Be("Latitude e longitude inexistentes.");
        }

        [Fact]
        public async Task ObterAnunciosPorCoordenadaDeUsuarioAsync_ComCoordenadasDoUsuarioInexistentes_RetornaAnuncios()
        {
            //arrange
            //ObterPorCoordenadaAsync
            anuncioServiceMock.Setup(s => s.ObterPorCoordenadaAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync((IEnumerable<Anuncio>)null);

            //act
            var anuncio = new AnunciosController(logger.Object,
                anuncioServiceMock.Object);

            var coordenadas = new CoordenadasUsuarioGet()
            {
                Latitude = -99999,
                Longitude = -99999
            };

            var anunciosGetResponse =
                await anuncio.ObterAnunciosPorCoordenadaDeUsuarioAsync(coordenadas);

            //assert
            anunciosGetResponse.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DownloadImagemAsync_ComIdAnuncioZerado_RetornaNotFound()
        {
            //act
            var anuncio = new AnunciosController(logger.Object,
                anuncioServiceMock.Object);

            var imagemGetResponse =
                anuncio.DownloadImagemAsync(0);

            //assert
            imagemGetResponse.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DownloadImagemAsync_ComIdAnuncioValido_RetornaNotFound()
        {
            //arrange
            anuncioServiceMock
                .Setup(f => f.DownloadImagemAsync(It.IsAny<int>()))
                .Returns(new MemoryStream(new byte[] { 0 }));

            //act
            var anuncio = new AnunciosController(logger.Object,
                this.anuncioServiceMock.Object);

            var imagemGetResponse =
                anuncio.DownloadImagemAsync(1);

            //assert
            imagemGetResponse.Should().BeOfType<FileStreamResult>();
        }

        [Fact]
        public void DownloadImagemAsync_ComIdAnuncioInvalido_RetornaNotFound()
        {
            //arrange

            var anuncioServiceMock = new Mock<IAnunciosService>();

            anuncioServiceMock
                .Setup(f => f.DownloadImagemAsync(It.IsAny<int>()))
                .Returns((Stream)null);

            //act
            var anuncio = new AnunciosController(logger.Object,
                anuncioServiceMock.Object);

            var imagemGetResponse =
                anuncio.DownloadImagemAsync(9999);

            //assert
            imagemGetResponse.Should().BeOfType<NotFoundResult>();
        }
    }
}
