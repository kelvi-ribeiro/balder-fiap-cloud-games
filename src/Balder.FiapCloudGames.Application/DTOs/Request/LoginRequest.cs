using System.ComponentModel.DataAnnotations;

namespace Balder.FiapCloudGames.Application.DTOs.Request
{
    public sealed record LoginRequest(
        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        string Email,
        [Required(ErrorMessage = "Senha Inválida!")] string Password);
}