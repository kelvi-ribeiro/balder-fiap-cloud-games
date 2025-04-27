using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.User;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
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
}
