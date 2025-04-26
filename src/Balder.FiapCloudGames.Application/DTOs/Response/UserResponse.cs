namespace Balder.FiapCloudGames.Application.DTOs.Response
{
    public sealed record class UserResponse(Guid Id, string Name, string Email, string token);
}