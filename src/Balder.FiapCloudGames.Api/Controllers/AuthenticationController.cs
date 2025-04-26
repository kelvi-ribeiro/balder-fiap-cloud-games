using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequest login)
        {
            var user = await _authenticationService.Login(login);
            if (user == null)
                return Results.NotFound("Usuário ou senha inválidos");
            return Results.Ok(user);
        }
        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] UserRequest register)
        {
            await _authenticationService.Register(register);
            return Results.Created();
        }
    }
}
