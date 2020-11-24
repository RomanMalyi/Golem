namespace Golem.Core.Models.Settings
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public string JwtExpireMinutes { get; set; }
        public string RefreshTokenExpireMinutes { get; set; }
    }
}
