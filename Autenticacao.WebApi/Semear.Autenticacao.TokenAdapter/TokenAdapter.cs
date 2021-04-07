using Microsoft.IdentityModel.Tokens;
using Semear.Autenticacao.Domain.Adapters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Semear.Autenticacao.TokenAdapter
{
    public class TokenAdapter : ITokenAdapter
    {
        private readonly JwtConfiguration jwtConfiguration;

        public TokenAdapter(JwtConfiguration jwtConfiguration)
        {
            this.jwtConfiguration = jwtConfiguration ??
                throw new ArgumentNullException(nameof(jwtConfiguration));
        }

        public string Obter(string email, string senha)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, senha)
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtConfiguration.ExpiresMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(jwtConfiguration.Secret)), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
