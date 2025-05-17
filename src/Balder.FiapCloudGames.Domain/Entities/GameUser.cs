using Balder.FiapCloudGames.Domain.Shared.Entities;

namespace Balder.FiapCloudGames.Domain.Entities;

public class GameUser(Guid gameId, Guid userId) : Entity(Guid.NewGuid())
{
    public Guid GameId { get; private set; } = gameId;
    public Game? Game { get; private set; }
    public Guid UserId { get; private set; } = userId;
    public User? User { get; private set; }
}