namespace Semear.Autenticacao.Domain.Adapters
{
    public interface ITokenAdapter
    {
        string Obter(string email, string senha);
    }
}