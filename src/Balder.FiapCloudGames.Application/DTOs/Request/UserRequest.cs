using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Validations;

namespace Balder.FiapCloudGames.Application.DTOs.Request;


public sealed class UserRequest
{
    [Required(ErrorMessage = "Nome é obrigatório!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória!")]
    [CustomValidation(typeof(CustomValidation), nameof(CustomValidation.PasswordValidate))]
    public string Senha { get; set; }

    public UserRequest(string name, string email, string senha)
    {
        Name = name;
        Email = email;
        Senha = senha;
    }
}

