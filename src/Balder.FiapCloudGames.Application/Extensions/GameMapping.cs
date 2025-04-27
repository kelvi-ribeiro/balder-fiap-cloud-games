using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response.Game;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Extensions;
public static class GameMapping
{
    public static GameDto Map(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Description,
            game.Platform,
            game.CompanyName,
            game.Price);
    }

    public static Game Map(this GameRequest gameRequest)
    {
        return new Game(
            gameRequest.Name,
            gameRequest.Description,
            gameRequest.Platform,
            gameRequest.CompanyName,
            gameRequest.Price);
    }
}