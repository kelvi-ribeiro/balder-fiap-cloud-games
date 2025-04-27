using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        return await this.MakeSafeCallAsync(( ) => authenticationService.Login(login));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest register)
    {
        return await this.MakeSafeCallAsync(( ) => authenticationService.Register(register));
    }
}
