using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Utils;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Extensions
{
    public static class UserMapping
    {
        public static UserDto Map(this User user)
        {
            return new UserDto(user.Id, user.Name, user.Email);
        }

        public static User Map(this UserRequest registerRequest, string role = "user")
        {
            return new User(registerRequest.Name, registerRequest.Email, HashingService.HashPassword(registerRequest.Password), role);
        }
    }
}