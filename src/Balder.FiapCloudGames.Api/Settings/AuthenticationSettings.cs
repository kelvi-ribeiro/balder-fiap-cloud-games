namespace Balder.FiapCloudGames.Api.Settings
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
        public AuthenticationSettings()
        {
            
        }
    }
}
