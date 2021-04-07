namespace Semear.Autenticacao.TokenAdapter
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int ExpiresMinutes { get; set; }
    }
}
