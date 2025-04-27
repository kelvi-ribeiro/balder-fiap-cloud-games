using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.Game;

namespace Balder.FiapCloudGames.Application.Interfaces;

public interface IGameService
{
    Task<GetGameDetailResponse> GetGameById(Guid id);
    Task<GetGameDetailResponse> GetGameByName(string name);
    Task<GetAllGamesResponse> GetAllGames();
    Task<BaseResponse> CreateGame(GameRequest game);
    Task<BaseResponse> UpdateGame(GameRequest game, Guid id);
    Task<BaseResponse> DeleteGame(Guid id);
}
