using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Entities;
using Balder.FiapCloudGames.Domain.Shared.Validations;

namespace Balder.FiapCloudGames.Domain.Entities
{
    public sealed class User : Entity
    {
        #region Propriedades
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; private set; }
        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; private set; }
        [Required(ErrorMessage = "Senha é obrigatória!")]
        [CustomValidation(typeof(CustomValidation), nameof(CustomValidation.PasswordValidate))]
        public string Password { get; private set; }
        #endregion

        #region Construtor
        public User(string name, string email, string password) : base(Guid.NewGuid())
        {
            Name = name;
            Email = email;
            Password = password;
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