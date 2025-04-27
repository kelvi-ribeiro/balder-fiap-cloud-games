namespace Balder.FiapCloudGames.Application.DTOs.Response.Game;

public class GetAllGamesResponse : BaseResponse
{
    public IEnumerable<GameDto> Games { get; set; } = [];
}
