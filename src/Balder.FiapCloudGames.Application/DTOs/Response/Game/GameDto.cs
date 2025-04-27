

namespace Balder.FiapCloudGames.Application.DTOs.Response.Game;

public record GameDto(
    Guid Id,
    string Name,
    string Description,
    string Platform,
    string CompanyName,
    decimal Price);