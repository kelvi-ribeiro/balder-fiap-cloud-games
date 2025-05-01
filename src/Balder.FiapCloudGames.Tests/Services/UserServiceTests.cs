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
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepositoryMock.Object);
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
}