using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.Authentication;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginRequest login);
    Task<BaseResponse> Register(UserRequest register);
    string GenerateToken(User login);
}