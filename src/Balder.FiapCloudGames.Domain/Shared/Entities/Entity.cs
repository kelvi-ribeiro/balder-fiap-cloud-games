using System.ComponentModel.DataAnnotations;

namespace Balder.FiapCloudGames.Domain.Shared.Entities
{
    public abstract class Entity : IEquatable<Guid>
    {
        #region Propriedades
        public Guid Id { get; private set; }
        #endregion
        #region Construtor
        protected Entity(Guid id)
        {
            Id = id;
        }
        #endregion
        #region Métodos
        public bool Equals(Guid id) => Id == id;
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
