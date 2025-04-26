namespace Balder.FiapCloudGames.Api.Settings
{
    public interface IAuthenticationSettings
    {
        string Key { get; }
        string Issuer { get; }
        string Audience { get; }
        int ExpiresInMinutes { get; }
    }
}
