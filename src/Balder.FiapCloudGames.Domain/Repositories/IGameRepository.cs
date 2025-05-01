using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Domain.Repositories
{
    public interface IGameRepository
    {
        Task<Game?> GetGameById(Guid id);
        Task<Game?> GetGameByName(string name);
        Task<ICollection<Game>> GetAllGames();
        Task CreateGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}