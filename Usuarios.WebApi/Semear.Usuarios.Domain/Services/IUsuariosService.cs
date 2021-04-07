using Semear.Usuarios.Domain.Models;
using System.Threading.Tasks;

namespace Semear.Usuarios.Domain.Services
{
    public interface IUsuariosService
    {
        Task<int> NovoAsync(Usuario usuario);

        Task NovoAcessoAsync(string email);

        Task<Usuario> ObterUsuarioAsync(string email);
    }
}