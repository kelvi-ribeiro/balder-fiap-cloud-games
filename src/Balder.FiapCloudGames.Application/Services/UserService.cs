using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Balder.FiapCloudGames.Application.Extensions;
using Balder.FiapCloudGames.Application.Interfaces;
using Balder.FiapCloudGames.Domain.Repositories;

namespace Balder.FiapCloudGames.Application.Services
{
    //TODO -> FAZER OS TRATAMENTOS E REGRA DE NEGÓCIO
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<UserResponse>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return users.Select(u => u.Map()).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> GetUserById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                return user.Map();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<UserResponse> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                return user.Map();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateUser(UserRequest user)
        {
            try
            {
                var userToUpdate = user.Map("");
                var userFromDb = await _userRepository.GetUserById(userToUpdate.Id);
                if (userFromDb == null)
                {
                    throw new Exception("Usuário não encontrado!");
                }
                userFromDb.UpdateUser(userToUpdate);
                await _userRepository.UpdateUser(userFromDb);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }     
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var userToDelete = await _userRepository.GetUserById(id);
                if (userToDelete == null)
                {
                    throw new Exception("Usuário não encontrado!");
                }
                await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
