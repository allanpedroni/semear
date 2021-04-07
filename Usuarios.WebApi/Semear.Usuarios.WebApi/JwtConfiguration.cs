namespace Semear.Usuarios.WebApi
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int ExpiresMinutes { get; set; }
    }
}