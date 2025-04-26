using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> Login(LoginRequest login);
        Task<UserResponse> Register(UserRequest register);
        string GenerateToken(User login);
    }
}