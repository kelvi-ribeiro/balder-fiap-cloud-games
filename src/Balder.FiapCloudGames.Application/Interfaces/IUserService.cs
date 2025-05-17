using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.User;

namespace Balder.FiapCloudGames.Application.Interfaces;

public interface IUserService
{
    Task<GetUserDetailResponse> GetUserById(Guid id);
    Task<GetAllUsersResponse> GetAllUsers();
    Task<BaseResponse> UpdateUser(UserRequest user, Guid id);
    Task<BaseResponse> DeleteUser(Guid id);
    Task<BaseResponse> AddGame(AddGameToUserRequest userRequest, Guid authenticatedUserId, string role);
}
