
namespace Balder.FiapCloudGames.Domain.Shared.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; set; }
    protected Entity(Guid id)
    {
        Id = id;
    }
    public bool Equals(Guid id) => Id == id;
    public override int GetHashCode() => Id.GetHashCode();
}
