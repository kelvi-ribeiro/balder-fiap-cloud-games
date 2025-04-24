namespace Balder.FiapCloudGames.Application.DTOs.Request
{
    public sealed record class GameRequest(
        string Name, 
        string Description, 
        string Platform,
        string CompanyName, 
        decimal Price);
}