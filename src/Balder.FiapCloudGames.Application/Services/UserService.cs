using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(u => u.Map()).ToList();
        }

        public async Task<UserResponse> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            return user.Map();
        }
        public async Task CreateUser(UserRequest user)
        {
            var userToCreate = user.Map();
            await _userRepository.CreateUser(userToCreate);
        }
        public async Task UpdateUser(UserRequest user)
        {
            var userToUpdate = user.Map();
            var userFromDb = await _userRepository.GetUserById(userToUpdate.Id);
            if (userFromDb == null)
            {
                throw new Exception("Usuário não encontrado!");
            }
            userFromDb.UpdateUser(userToUpdate);
            await _userRepository.UpdateUser(userFromDb);
        }

        public async Task DeleteUser(Guid id)
        {
            var userToDelete = await _userRepository.GetUserById(id);
            if (userToDelete == null)
            {
                throw new Exception("Usuário não encontrado!");
            }
            await _userRepository.DeleteUser(id);
        }
    }
}
