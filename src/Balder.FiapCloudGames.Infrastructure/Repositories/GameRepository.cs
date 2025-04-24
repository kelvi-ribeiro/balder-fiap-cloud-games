using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Balder.FiapCloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Balder.FiapCloudGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Game>> GetAllGames()
        {
            return await _context.Games.AsNoTracking().ToListAsync();
        }

        public async Task<Game?> GetGameById(Guid id)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<Game?> GetGameByName(string name)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task CreateGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGame(Game game)
        {
            var gameToUpdate = await _context.Games.FindAsync(game.Id);
            if (gameToUpdate == null)
            {
                throw new Exception("Jogo não encontrado!");
            }
            gameToUpdate!.UpdateGame(game);
            _context.Games.Update(gameToUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGame(Guid id)
        {
            var gameToDelete = await _context.Games.FindAsync(id);
            if (gameToDelete == null)
            {
                throw new Exception("Jogo não encontrado!");
            }
            _context.Games.Remove(gameToDelete);
            await _context.SaveChangesAsync();
        }
    }
}