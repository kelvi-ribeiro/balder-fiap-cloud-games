using System.ComponentModel.DataAnnotations;

namespace Balder.FiapCloudGames.Application.DTOs.Request;

public sealed record AddGameToUserRequest(
    [Required(ErrorMessage = "O usuário é obrigatório!")]
    Guid UserId,
    [Required(ErrorMessage = "O jogo é obrigatório!")]
    Guid GameId);