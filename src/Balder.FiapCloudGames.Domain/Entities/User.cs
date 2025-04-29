using Balder.FiapCloudGames.Domain.Shared.Entities;

namespace Balder.FiapCloudGames.Domain.Entities
{
    public sealed class User : Entity
    {
        #region Propriedades
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        #endregion  
        #region Construtor
        public User(string name, string email, string password, string role) : base(Guid.NewGuid())
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
        public User(string id, string name, string email, string password, string role) : base(Guid.Parse(id))
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
        #endregion
    }
}