using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;

namespace Balder.FiapCloudGames.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(Guid id);
        Task<UserResponse> GetUserByEmail(string email);
        Task<ICollection<UserResponse>> GetAllUsers();
        Task UpdateUser(UserRequest user);
        Task DeleteUser(Guid id);
    }
}