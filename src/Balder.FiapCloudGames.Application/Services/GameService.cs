using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.Game;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GetAllGamesResponse> GetAllGames()
    {

        var games = await _gameRepository.GetAllGames();
        return new GetAllGamesResponse
        {
            Games = games.Select(g => new GameDto(
                g.Id,
                g.Name,
                g.Description,
                g.Platform,
                g.CompanyName,
                g.Price))
        };
    }

    public async Task<GetGameDetailResponse> GetGameById(Guid id)
    {
        var response = new GetGameDetailResponse();
        var game = await _gameRepository.GetGameById(id);
        if (game == null)
        {
            response.AddError("GAME_NOT_FOUND", $"Game with id {id} not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Game = game.Map();
        return response;
    }

    public async Task<GetGameDetailResponse> GetGameByName(string name)
    {

        var response = new GetGameDetailResponse();
        var game = await _gameRepository.GetGameByName(name);
        if (game == null)
        {
            response.AddError("GAME_NOT_FOUND", $"Game with name '{name}' not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Game = game.Map();
        return response;
    }

    public async Task<BaseResponse> CreateGame(GameRequest game)
    {
        var newGame = new Domain.Entities.Game(
            game.Name,
            game.Description,
            game.Platform,
            game.CompanyName,
            game.Price);
        await _gameRepository.CreateGame(newGame);
        return new BaseResponse();
    }

    public async Task<BaseResponse> UpdateGame(GameRequest game, Guid id)
    {
        var response = new BaseResponse();

        var existingGame = await _gameRepository.GetGameById(id);
        if (existingGame == null)
        {
            response.AddError("GAME_NOT_FOUND", $"Game with id '{id}' not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        existingGame.UpdateGame(new Domain.Entities.Game(
            game.Name,
            game.Description,
            game.Platform,
            game.CompanyName,
            game.Price));
        await _gameRepository.UpdateGame(existingGame);
        return response;
    }

    public async Task<BaseResponse> DeleteGame(Guid id)
    {
        var response = new BaseResponse();

        var existingGame = await _gameRepository.GetGameById(id);
        if (existingGame == null)
        {
            response.AddError("GAME_NOT_FOUND", $"Game with id '{id}' not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        await _gameRepository.DeleteGame(existingGame.Id);
        return response;
    }
}