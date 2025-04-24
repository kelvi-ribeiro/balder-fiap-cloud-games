using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Application.Extensions
{
    public static class UserMapping
    {
        public static UserResponse Map(this User user)
        {
            return new UserResponse(user.Id, user.Name, user.Email);
        }

        public static User Map(this UserRequest userRequest)
        {
            return new User(userRequest.Name, userRequest.Email, userRequest.Senha);
        }

    }
}