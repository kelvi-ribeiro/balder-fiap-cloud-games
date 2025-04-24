using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Validations;

namespace Balder.FiapCloudGames.Application.DTOs.Request;

public sealed record UserRequest(
    [property: Required(ErrorMessage = "Nome é obrigatório!")] string Name,
    [property: Required(ErrorMessage = "Email é obrigatório!")]
    [property: EmailAddress(ErrorMessage = "Email inválido!")] string Email,
    [property: Required(ErrorMessage = "Senha é obrigatória!")]
    [property: CustomValidation(typeof(CustomValidation), nameof(CustomValidation.PasswordValidate))] string Senha
);