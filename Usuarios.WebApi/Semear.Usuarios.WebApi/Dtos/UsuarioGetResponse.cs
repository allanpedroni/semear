using Semear.Usuarios.Domain.Models;

namespace Semear.Usuarios.WebApi.Dtos
{
    public class UsuarioGetResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string Senha { get; set; }
    }

    public static class UsuarioMappingExtensions
    {
        public static UsuarioGetResponse ToViewModel(this Usuario src)
        {
            return new UsuarioGetResponse
            {
                Id = src.Id,
                Nome = src.Nome,
                Email = src.Email,
                Senha = src.Senha,
                Ativo = src.Ativo
            };
        }
    }
}