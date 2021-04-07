using System.IO;

namespace Semear.Anuncios.Domain.Models
{
    public class Anuncio
    {
        public int Id { get; private set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TotalAcessos { get; set; }

        public Anuncio()
        {

        }
        public Anuncio(string texto, string imagem, double latitude, double longitude)
        {
            Texto = texto;
            Imagem = imagem;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
