
namespace Balder.FiapCloudGames.Application.DTOs.Response.User;

public class GetAllUsersResponse : BaseResponse
{
    public IEnumerable<UserDto>? Users { get; set; }
}
