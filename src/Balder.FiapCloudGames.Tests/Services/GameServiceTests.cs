using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Services;
using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Moq;

namespace Balder.FiapCloudGames.Tests.Services;

public class GameServiceTests
{
    private readonly Mock<IGameRepository> _gameRepositoryMock = new();
    private readonly IGameService _gameService;

    public GameServiceTests()
    {
        _gameService = new GameService(_gameRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllGames_ShouldReturnGames_WhenGamesExist()
    {
        // Arrange
        var games = new List<Game>
        {
            new ("Game1", "Description1", "Platform1", "Company1", 10.0m),
            new ("Game2", "Description2", "Platform2", "Company2", 20.0m)
        };
        _gameRepositoryMock.Setup(repo => repo.GetAllGames()).ReturnsAsync(games);

        // Act
        var response = await _gameService.GetAllGames();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(2, response.Games!.Count());
        Assert.Equal("Game1", response.Games!.First().Name);
    }

    [Fact]
    public async Task GetGameById_ShouldReturnGame_WhenGameExists()
    {
        // Arrange
        var game = new Game("Game1", "Description1", "Platform1", "Company1", 10.0m);
        _gameRepositoryMock.Setup(repo => repo.GetGameById(It.IsAny<Guid>())).ReturnsAsync(game);

        // Act
        var response = await _gameService.GetGameById(It.IsAny<Guid>());

        // Assert
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task GetGameById_ShouldReturnError_WhenGameDoesNotExist()
    {
        // Arrange
        _gameRepositoryMock
            .Setup(repo => repo.GetGameById(It.IsAny<Guid>()))
            .ReturnsAsync((Game?)null);

        // Act
        var response = await _gameService.GetGameById(It.IsAny<Guid>());

        // Assert
        Assert.False(response.IsSuccessful);
        Assert.Null(response.Game);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "GAME_NOT_FOUND");
    }

    [Fact]
    public async Task CreateGame_ShouldCallRepository_WhenGameIsValid()
    {
        // Arrange
        var gameRequest = new GameRequest("game1", "description1", "platform1", "company1", 10.0m);

        // Act
        var response = await _gameService.CreateGame(gameRequest);

        // Assert
        _gameRepositoryMock.Verify(repo => repo.CreateGame(It.IsAny<Game>()), Times.Once);
        Assert.NotNull(response);
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task UpdateGame_ShouldUpdateGame_WhenGameExists()
    {
        // Arrange
        var existingGame = new Game("Game1", "Description1", "Platform1", "Company1", 10.0m);
        var gameRequest = new GameRequest("UpdatedGame", "UpdatedDescription", "UpdatedPlatform", "UpdatedCompany", 15.0m);

        _gameRepositoryMock.Setup(repo => repo.GetGameById(It.IsAny<Guid>())).ReturnsAsync(existingGame);

        // Act
        var response = await _gameService.UpdateGame(gameRequest, It.IsAny<Guid>());

        // Assert
        _gameRepositoryMock.Verify(repo => repo.UpdateGame(It.IsAny<Game>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGame_ShouldDeleteGame_WhenGameExists()
    {
        // Arrange
        var existingGame = new Game("Game1", "Description1", "Platform1", "Company1", 10.0m);

        _gameRepositoryMock.Setup(repo => repo.GetGameById(It.IsAny<Guid>())).ReturnsAsync(existingGame);

        // Act
        var response = await _gameService.DeleteGame(It.IsAny<Guid>());

        // Assert
        _gameRepositoryMock.Verify(repo => repo.DeleteGame(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGame_ShouldReturnError_WhenGameDoesNotExist()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _gameRepositoryMock.Setup(repo => repo.GetGameById(gameId)).ReturnsAsync((Game?)null);

        // Act
        var response = await _gameService.DeleteGame(gameId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "GAME_NOT_FOUND");
    }
}