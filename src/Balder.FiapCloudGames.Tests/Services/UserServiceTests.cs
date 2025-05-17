using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Services;
using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Moq;

namespace Balder.FiapCloudGames.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IGameRepository> _gameRepositoryMock = new();
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepositoryMock.Object, _gameRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new("User1", "user1@example.com", "password", "user"),
            new("User2", "user2@example.com", "password", "user")
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

        // Act
        var response = await _userService.GetAllUsers();

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessful);
        Assert.Equal("User1", response.Users!.First().Name);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User("User1", "user1@example.com", "password", "user");
        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).ReturnsAsync(user);

        // Act
        var response = await _userService.GetUserById(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock
            .Setup(repo => repo.GetUserById(It.IsAny<Guid>()))
            .ReturnsAsync((User?)null);

        // Act
        var response = await _userService.GetUserById(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.User);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.False(response.IsSuccessful);
        Assert.Contains(response.Errors!, e => e.Code == "USER_NOT_FOUND");
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser_WhenUserExists()
    {
        // Arrange
        var existingUser = new User("User1", "user1@example.com", "password", "user");
        var userRequest = new UserRequest("UpdatedUser", "teste@teste.com", "password");

        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).ReturnsAsync(existingUser);

        // Act
        var response = await _userService.UpdateUser(userRequest, It.IsAny<Guid>());

        // Assert
        _userRepositoryMock.Verify(repo => repo.UpdateUser(It.IsAny<User>()), Times.Once);
        Assert.NotNull(response);
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var userRequest = new UserRequest("UpdatedUser", "teste@teste.com", "password");

        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);

        // Act
        var response = await _userService.UpdateUser(userRequest, It.IsAny<Guid>());

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessful);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "USER_NOT_FOUND");
    }

    [Fact]
    public async Task DeleteUser_ShouldDeleteUser_WhenUserExists()
    {
        // Arrange
        var existingUser = new User("User1", "user1@example.com", "password", "user");

        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).ReturnsAsync(existingUser);

        // Act
        var response = await _userService.DeleteUser(It.IsAny<Guid>());

        // Assert
        _userRepositoryMock.Verify(repo => repo.DeleteUser(It.IsAny<Guid>()), Times.Once);
        Assert.True(response.IsSuccessful);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);

        // Act
        var response = await _userService.DeleteUser(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "USER_NOT_FOUND");
    }

    [Fact]
    public async Task AddGame_ShouldAddGame_WhenUserAndGameExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var authenticatedUser = userId;
        var gameId = Guid.NewGuid();
        var userRequest = new AddGameToUserRequest(userId, gameId);
        var existingUser = new User("User1", "user1@example.com", "password", "user");
        var existingGame = new Game("Game1", "Description", "PC", "Company", 59.99m);

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
        _gameRepositoryMock.Setup(repo => repo.GetGameById(gameId)).ReturnsAsync(existingGame);

        // Act
        var response = await _userService.AddGame(userRequest, authenticatedUser, "user");

        // Assert
        _userRepositoryMock.Verify(repo => repo.AddGame(userId, gameId), Times.Once);
        Assert.NotNull(response);
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task AddGame_ShouldReturnError_WhenUserTryToAddGameInAnotherUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var authenticatedUser = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        var userRequest = new AddGameToUserRequest(userId, gameId);

        // Act
        var response = await _userService.AddGame(userRequest, authenticatedUser, "user");

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessful);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "USER_NOT_ALLWED_TO_ADD_GAME_IN_ANOTHER_USER");
    }

    [Fact]
    public async Task AddGame_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var authenticatedUser = userId;
        var gameId = Guid.NewGuid();
        var userRequest = new AddGameToUserRequest(userId, gameId);

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User?)null);

        // Act
        var response = await _userService.AddGame(userRequest, authenticatedUser, "user");

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessful);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "USER_NOT_FOUND");
    }

    [Fact]
    public async Task AddGame_ShouldReturnError_WhenGameDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var authenticatedUser = userId;
        var gameId = Guid.NewGuid();
        var userRequest = new AddGameToUserRequest(userId, gameId);
        var existingUser = new User("User1", "user1@example.com", "password", "user");

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
        _gameRepositoryMock.Setup(repo => repo.GetGameById(gameId)).ReturnsAsync((Game?)null);

        // Act
        var response = await _userService.AddGame(userRequest, authenticatedUser, "user");

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessful);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(response.Errors!, e => e.Code == "GAME_NOT_FOUND");
    }
}