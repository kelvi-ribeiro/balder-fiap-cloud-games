
using System.Text.Json.Serialization;

namespace Balder.FiapCloudGames.Application.DTOs.Response.Authentication;

public class LoginResponse : BaseResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AccessToken { get; set; }
}
