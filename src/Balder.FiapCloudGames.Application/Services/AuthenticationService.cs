using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Balder.FiapCloudGames.Api.Settings;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.DTOs.Response.Authentication;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Utils;
using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Balder.FiapCloudGames.Application.Services;

public class AuthenticationService(IUserRepository userRepository, IAuthenticationSettings authenticationSettings) : IAuthenticationService
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: authenticationSettings.Issuer,
            audience: authenticationSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(authenticationSettings.ExpiresInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);
    }

    public async Task<LoginResponse> Login(LoginRequest login)
    {
        var loginResponse = new LoginResponse();
        var user = await userRepository.GetUserByEmail(login.Email);
        if (user == null || !HashingService.VerifyPassword(login.Password, user.Password))
        {
            loginResponse.AddError("INVALID_CREDENTIALS", "Invalid e-mail or password.");
            return loginResponse;

        }

        var token = GenerateToken(user);
        loginResponse.AccessToken = token;
        return loginResponse;
    }

    public async Task<BaseResponse> Register(UserRequest register)
    {
        var response = new BaseResponse();
        var userToCreate = await userRepository.GetUserByEmail(register.Email);
        if (userToCreate != null)
        {
            response.AddError("USER_ALREADY_EXISTS", $"User with email {register.Email} already exists.");
            return response;
        }

        userToCreate = register.Map();
        await userRepository.CreateUser(userToCreate);
        return response;
    }
}