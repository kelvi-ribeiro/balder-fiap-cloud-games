using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<ICollection<GameResponse>> GetAllGames()
        {
            var games = await _gameRepository.GetAllGames();
            return games.Select(g => new GameResponse(
                g.Id,
                g.Name,
                g.Description,
                g.Platform,
                g.CompanyName,
                g.Price)).ToList();
        }

        public async Task<GameResponse> GetGameById(Guid id)
        {
            var game = await _gameRepository.GetGameById(id);
            if (game == null)
                throw new Exception($"Jogo não encontrado!");
            return new GameResponse(
                game.Id,
                game.Name,
                game.Description,
                game.Platform,
                game.CompanyName,
                game.Price);
        }
        public async Task<GameResponse> GetGameByName(string name)
        {
            var game = await _gameRepository.GetGameByName(name);
            if (game == null)
                throw new Exception($"Jogo não encontrado!");
            return new GameResponse(
                game.Id,
                game.Name,
                game.Description,
                game.Platform,
                game.CompanyName,
                game.Price);
        }
        public async Task CreateGame(GameRequest game)
        {
            var newGame = new Domain.Entities.Game(
                game.Name,
                game.Description,
                game.Platform,
                game.CompanyName,
                game.Price);
            await _gameRepository.CreateGame(newGame);
        }
        public async Task UpdateGame(GameRequest game)
        {
            var existingGame = await _gameRepository.GetGameByName(game.Name);
            if (existingGame == null)
                throw new KeyNotFoundException($"Jogo ja cadastrado");
            existingGame.UpdateGame(new Domain.Entities.Game(
                game.Name,
                game.Description,
                game.Platform,
                game.CompanyName,
                game.Price));
            await _gameRepository.UpdateGame(existingGame);
        }

        public async Task DeleteGame(Guid id)
        {
            var game = await _gameRepository.GetGameById(id);
            if (game == null)
                throw new KeyNotFoundException($"Game with id {id} not found.");
            await _gameRepository.DeleteGame(game.Id);
        }

    }
}