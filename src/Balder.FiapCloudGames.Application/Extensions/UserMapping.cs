using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Utils;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Extensions
{
    public static class UserMapping
    {
        public static UserResponse Map(this User user)
        {
            return new UserResponse(user.Id, user.Name, user.Email,string.Empty);
        }

        public static User Map(this UserRequest registerRequest, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                role = "user";
            }
            return new User(registerRequest.Name, registerRequest.Email, HashingService.HashPassword(registerRequest.Password), role);
        }

    }
}