using Balder.FiapCloudGames.Domain.Shared.Entities;

namespace Balder.FiapCloudGames.Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        #region Construtor
        public User(string name, string email, string password, string role) : base(Guid.NewGuid())
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
        #endregion
        #region Métodos
        public void UpdateUser(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
        }
        #endregion
    }
}