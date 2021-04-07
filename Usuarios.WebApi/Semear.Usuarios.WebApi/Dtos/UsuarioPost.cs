using Semear.Usuarios.Domain.Models;

namespace Semear.Usuarios.WebApi.Dtos
{
    public class UsuarioPost
    {
        public string Nome { get; set; }
        //TODO: Atributo email aceitando apenas emails válidos
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario ToModel()
        {
            return new()
            {
                Email = Email,
                Nome = Nome,
                Senha = Senha
            };
        }
    }
}