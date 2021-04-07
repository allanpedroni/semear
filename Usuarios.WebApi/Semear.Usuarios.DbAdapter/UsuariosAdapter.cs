using Dapper;
using Semear.Usuarios.Domain.Adapters;
using Semear.Usuarios.Domain.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Semear.Usuarios.DbAdapter
{
    public class UsuariosAdapter : IUsuariosAdapter
    {
        private readonly IDbConnection dbConnection;
        static UsuariosAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public UsuariosAdapter(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ??
                throw new ArgumentNullException(nameof(dbConnection));
        }

        private async Task<bool> ExisteUsuarioAsync(string email)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Email", email);

            var query =@"SELECT 1 FROM Usuario WHERE Email = @Email";

            return await dbConnection
                .ExecuteScalarAsync<bool>(query, parametros);
        }

        public async Task NovoAcessoAsync(string email)
        {
            var existeUsuario = await ExisteUsuarioAsync(email);

            if (existeUsuario)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Email", email);

                var query = @"
                    UPDATE Usuario
                    SET Ativo=1
                    WHERE Email = @Email";

                await dbConnection.ExecuteAsync(query, parametros);
            }
            else
            {
                throw new UsuarioInexistenteException();
            }
        }

        public async Task<int> NovoAsync(Usuario usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Nome", usuario.Nome);
            parametros.Add("@Email", usuario.Email);
            parametros.Add("@Senha", usuario.Senha);

            var query = @"
                INSERT INTO Usuario(Nome, Email, Senha, Ativo)
                OUTPUT Inserted.Id
                VALUES(@Nome, @Email, @Senha, 0)";

            var idUsuario = await dbConnection.ExecuteScalarAsync<int>(query, parametros);

            return idUsuario;
        }

        public async Task<Usuario> ObterUsuarioAsync(string email)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Email", email);

            var query =
                "SELECT Id, Nome, Email, Senha, Ativo FROM Usuario WHERE Email = @Email";

            return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(query, parametros);
        }
    }
}
