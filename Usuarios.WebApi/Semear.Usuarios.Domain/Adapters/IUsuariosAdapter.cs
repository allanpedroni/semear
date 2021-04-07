using Semear.Usuarios.Domain.Models;
using System.Threading.Tasks;

namespace Semear.Usuarios.Domain.Adapters
{
    public interface IUsuariosAdapter
    {
        Task<int> NovoAsync(Usuario usuario);

        Task NovoAcessoAsync(string email);

        Task<Usuario> ObterUsuarioAsync(string email);
    }
}