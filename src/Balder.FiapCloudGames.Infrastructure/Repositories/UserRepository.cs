using System.Data.Common;
using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Balder.FiapCloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Balder.FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context
                .Users
                .Include(u => u.GameUsers)
                .ThenInclude(gu => gu.Game)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context
                .Users
                .Include(u => u.GameUsers)
                .ThenInclude(gu => gu.Game)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete == null)
            {
                throw new Exception("Usuário não encontrado!");
            }
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task AddGame(Guid userId, Guid gameId)
        {
            var gameUser = new GameUser(gameId, userId);
            _context.GameUsers.Add(gameUser);
            await _context.SaveChangesAsync();
        }
    }
}