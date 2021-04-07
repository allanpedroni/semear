using System.IO;

namespace Semear.Autenticacao.Domain.Models
{
    public class Anuncio
    {
        public int Id { get; private set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        public int IdCliente { get; set; }
        public int TotalAcessos { get; set; }

        public Anuncio()
        {

        }
        public Anuncio(int id, string texto, string imagem, int idCliente, int totalAcessos)
        {
            Id = id;
            Texto = texto;
            Imagem = imagem;
            IdCliente = idCliente;
            TotalAcessos = totalAcessos;
        }
    }
}
