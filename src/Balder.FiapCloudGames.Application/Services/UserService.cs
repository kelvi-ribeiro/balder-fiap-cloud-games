using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.User;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services;

public class UserService(IUserRepository userRepository, IGameRepository gameRepository) : IUserService
{
    public async Task<GetAllUsersResponse> GetAllUsers()
    {
        var response = new GetAllUsersResponse();
        var users = await userRepository.GetAllUsers();
        response.Users = users.Select(u => u.Map()).ToList();
        return response;
    }

    public async Task<GetUserDetailResponse> GetUserById(Guid id)
    {
        var response = new GetUserDetailResponse();
        var user = await userRepository.GetUserById(id);
        if (user == null)
        {
            response.AddError("USER_NOT_FOUND", $"Game with id {id} not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }
        response.User = user.Map();
        response.Games = user.GameUsers
            .Where(g => g.Game != null)
            .Select(g => g.Game!.Map())
            .ToList();
        return response;
    }

    public async Task<BaseResponse> UpdateUser(UserRequest userRequest, Guid id)
    {
        var response = new GetUserDetailResponse();
        var existentUser = await userRepository.GetUserById(id);
        if (existentUser == null)
        {
            response.AddError("USER_NOT_FOUND", $"Game with id '{id}' not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }
        var updatedUser = userRequest.Map();
        updatedUser.Id = id;    
        await userRepository.UpdateUser(updatedUser);
        return response;
    }

    public async Task<BaseResponse> DeleteUser(Guid id)
    {
       var response = new BaseResponse();
        var existentUser = await userRepository.GetUserById(id);
        if (existentUser == null)
        {
            response.AddError("USER_NOT_FOUND", $"Game with id '{id}' not found.");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }
        await userRepository.DeleteUser(id);
        return response;
    }

    public async Task<BaseResponse> AddGame(AddGameToUserRequest userRequest, Guid authenticatedUserId, string role)
    {
        var response = new BaseResponse();
        if (role != "admin" && authenticatedUserId != userRequest.UserId)
        {
            response.AddError("USER_NOT_ALLWED_TO_ADD_GAME_IN_ANOTHER_USER", "You are not authorized to add a game to this user.");
            response.StatusCode = HttpStatusCode.Forbidden;
            return response;
        }
        var user = await userRepository.GetUserById(userRequest.UserId);
        if (user == null)
        {
            response.AddError("USER_NOT_FOUND", $"User with id '{userRequest.UserId}' not found.");
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }
        var game = await gameRepository.GetGameById(userRequest.GameId);
        if (game == null)
        {
            response.AddError("GAME_NOT_FOUND", $"Game with id '{userRequest.GameId}' not found.");
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }

        if(user.GameUsers.Any(g => g.GameId == userRequest.GameId))
        {
            response.AddError("GAME_ALREADY_ADDED", $"Game with id '{userRequest.GameId}' already added to user.");
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }
        
        await userRepository.AddGame(userRequest.UserId, userRequest.GameId);
        return response;
    }
}
