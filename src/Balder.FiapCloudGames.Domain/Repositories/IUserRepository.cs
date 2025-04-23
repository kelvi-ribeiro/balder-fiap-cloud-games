using Balder.FiapCloudGames.Domain.Entities;

namespace Balder.FiapCloudGames.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<ICollection<User>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}
