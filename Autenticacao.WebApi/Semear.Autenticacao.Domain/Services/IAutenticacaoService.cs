using System.Threading.Tasks;

namespace Semear.Autenticacao.Domain.Services
{
    public interface IAutenticacaoService
    {
        Task<string> ObterToken(string email, string senha);
    }
}