namespace Balder.FiapCloudGames.Application.DTOs.Response
{
    public sealed record class GameResponse(
        Guid Id,
        string Name,
        string Description,
        string Platform,
        string CompanyName,
        decimal Price);
}
