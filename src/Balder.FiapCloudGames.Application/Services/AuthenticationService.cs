using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Balder.FiapCloudGames.Api.Settings;
using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Application.Utils;
using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Balder.FiapCloudGames.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthenticationSettings authenticationSettings;

        public AuthenticationService(IUserRepository userRepository, IAuthenticationSettings authenticationSettings)
        {
            this.userRepository = userRepository;
            this.authenticationSettings = authenticationSettings;
        }

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

        public async Task<UserResponse> Login(LoginRequest login)
        {
            var user = await userRepository.GetUserByEmail(login.Email);
            if (user == null)
                throw new Exception("Usuário não encontrado!");
            if (!HashingService.VerifyPassword(login.Password, user.Password))
                throw new Exception("Senha inválida!");
            var token = GenerateToken(user);
            return new UserResponse(user.Id, user.Name, user.Email, token);
        }

        public async Task<UserResponse> Register(UserRequest register)
        {
            try
            {
                var userToCreate = await userRepository.GetUserByEmail(register.Email);
                if (userToCreate != null)
                {
                    throw new Exception("Email já cadastrado!");
                }
                userToCreate = register!.Map("");
                await userRepository.CreateUser(userToCreate);
                return(userToCreate.Map());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}