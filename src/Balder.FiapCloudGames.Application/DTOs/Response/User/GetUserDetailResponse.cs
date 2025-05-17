
using Balder.FiapCloudGames.Application.DTOs.Response.Game;

namespace Balder.FiapCloudGames.Application.DTOs.Response.User;

public class GetUserDetailResponse : BaseResponse
{
    public UserDto? User { get; set; }
    public List<GameDto> Games { get; set; } = [];
}
