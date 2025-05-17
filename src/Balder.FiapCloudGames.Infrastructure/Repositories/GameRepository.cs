using Balder.FiapCloudGames.Domain.Entities;
using Balder.FiapCloudGames.Domain.Repositories;
using Balder.FiapCloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Balder.FiapCloudGames.Infrastructure.Repositories;

public class GameRepository(ApplicationDbContext context) : IGameRepository
{
    public async Task<ICollection<Game>> GetAllGames()
    {
        return await context.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetGameById(Guid id)
    {
        return await context
            .Games
            .Include(g => g.GameUsers)
            .ThenInclude(gu => gu.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    public async Task<Game?> GetGameByName(string name)
    {
        return await context
            .Games
            .Include(g => g.GameUsers)
            .ThenInclude(gu => gu.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Name == name);
    }

    public async Task CreateGame(Game game)
    {
        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
    }
    public async Task UpdateGame(Game game)
    {
        var gameToUpdate = await context.Games.FindAsync(game.Id);
        if (gameToUpdate == null)
        {
            throw new Exception("Jogo não encontrado!");
        }
        gameToUpdate!.UpdateGame(game);
        context.Games.Update(gameToUpdate);
        await context.SaveChangesAsync();
    }
    public async Task DeleteGame(Guid id)
    {
        var gameToDelete = await context.Games.FindAsync(id);
        if (gameToDelete == null)
        {
            throw new Exception("Jogo não encontrado!");
        }
        context.Games.Remove(gameToDelete);
        await context.SaveChangesAsync();
    }
}