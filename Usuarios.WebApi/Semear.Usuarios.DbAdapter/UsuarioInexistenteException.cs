using System;
using System.Runtime.Serialization;

namespace Semear.Usuarios.DbAdapter
{
    [Serializable]
    internal class UsuarioInexistenteException : Exception
    {
        public UsuarioInexistenteException()
        {
        }

        public UsuarioInexistenteException(string message) : base("Não existe o usuário cadastrado.")
        {
        }

        public UsuarioInexistenteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsuarioInexistenteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}