using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;

namespace Balder.FiapCloudGames.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameResponse> GetGameById(Guid id);
        Task<GameResponse> GetGameByName(string name);
        Task<ICollection<GameResponse>> GetAllGames();
        Task CreateGame(GameRequest game);
        Task UpdateGame(GameRequest game);
        Task DeleteGame(Guid id);
    }
}