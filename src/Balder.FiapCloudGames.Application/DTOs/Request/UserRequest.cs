using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Validations;

namespace Balder.FiapCloudGames.Application.DTOs.Request;

public sealed record UserRequest(
    [Required(ErrorMessage = "Nome é obrigatório!")]
    string Name,
    [Required(ErrorMessage = "Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    string Email,
    [Required(ErrorMessage = "Senha é obrigatória!")]
    [CustomValidation(typeof(CustomValidation), nameof(CustomValidation.PasswordValidate))]
    string Password
);