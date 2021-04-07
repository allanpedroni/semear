using Microsoft.AspNetCore.Http;
using Semear.Anuncios.Domain.Models;

namespace Semear.Anuncios.WebApi.Dtos
{
    public class AnuncioGetResponse
    {
        public string Texto { get; init; }
        public string ImagemUrl { get; init; }
        public int TotalAcessos { get; init; }
    }

    public static class AnuncioMappingExtensions
    {
        //Poderia ser usado AutoMapper ao invés da extension pattern.
        public static AnuncioGetResponse ToViewModel(this Anuncio src, HttpRequest request)
        {
            var scheme = request.Host.Host.Contains("localhost") ? request.Scheme : "https";
            var urlBaseImagemDownload = $"{scheme}://{request.Host}{request.Path}";

            return new AnuncioGetResponse
            {
                Texto = src.Texto,
                ImagemUrl = $"{urlBaseImagemDownload}/{src.Id}/Imagem",
                TotalAcessos = src.TotalAcessos
            };
        }
    }
}