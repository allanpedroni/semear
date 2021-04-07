namespace Semear.Autenticacao.Domain.Models
{
    public class Usuario
    {
        public int Id { get; init; }
        public string Nome { get; init; }
        //TODO: Atributo email aceitando apenas emails válidos
        public string Email { get; init; }
        public bool Ativo { get; init; }
    }
}
