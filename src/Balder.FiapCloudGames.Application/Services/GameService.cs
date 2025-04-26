using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services
{
    //TODO -> FAZER OS TRATAMENTOS E REGRA DE NEGÓCIO
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<ICollection<GameResponse>> GetAllGames()
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<GameResponse> GetGameById(Guid id)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<GameResponse> GetGameByName(string name)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task CreateGame(GameRequest game)
        {
            try
            {
                var newGame = new Domain.Entities.Game(
                    game.Name,
                    game.Description,
                    game.Platform,
                    game.CompanyName,
                    game.Price);
                await _gameRepository.CreateGame(newGame);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateGame(GameRequest game)
        {
            try
            {
                var existingGame = await _gameRepository.GetGameByName(game.Name);
                if (existingGame == null)
                    throw new KeyNotFoundException($"Jogo não encontrado");
                existingGame.UpdateGame(new Domain.Entities.Game(
                    game.Name,
                    game.Description,
                    game.Platform,
                    game.CompanyName,
                    game.Price));
                await _gameRepository.UpdateGame(existingGame);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteGame(Guid id)
        {
            try
            {
                var game = await _gameRepository.GetGameById(id);
                if (game == null)
                    throw new KeyNotFoundException($"Jogo não encontrado.");
                await _gameRepository.DeleteGame(game.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}