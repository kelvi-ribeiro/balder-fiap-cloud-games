using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : BaseController
{
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login do Usuario", Description = "Endpoint para fazer o login do usuario usando Email e Senha seguindo os padrões de autenticação")]
    [ProducesResponseType(StatusCodes.Status200OK)] // Sucesso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Erro de validação
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Não autorizado
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        return await this.MakeSafeCallAsync(() => authenticationService.Login(login));
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Criar um novo usuario", Description = "Endpoint para criar um novo usuario seguindo os padrões de autenticação")]
    [ProducesResponseType(StatusCodes.Status201Created)] // Criado com sucesso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Erro de validação
    [ProducesResponseType(StatusCodes.Status409Conflict)] // Conflito, usuário já existente por exemplo
    public async Task<IActionResult> Register([FromBody] UserRequest register)
    {
        return await this.MakeSafeCallAsync(() => authenticationService.Register(register));
    }

}
