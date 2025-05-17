using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService userService) : BaseController
{
    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Visualizar Usuario", Description = "Endpoint para pegar o usuário por ID já pré-definido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        return await this.MakeSafeCallAsync(() => userService.GetUserById(id));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Visualizar todos os Usuários", Description = "Endpoint para visualizar todos os usuários já cadastrados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        return await this.MakeSafeCallAsync(userService.GetAllUsers);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Atualização de Usuário", Description = "Endpoint para atualizar usuário baseado no ID")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserRequest user, Guid id)
    {
        return await this.MakeSafeCallAsync(() => userService.UpdateUser(user, id));
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Deletar Usuário", Description = "Endpoint para deletar um usuário por ID pré-definido")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        return await this.MakeSafeCallAsync(() => userService.DeleteUser(id));
    }

    [HttpPost("games")]
    [SwaggerOperation(Summary = "Adicionar game", Description = "Endpoint para adicionar um game ao usuário")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> AddGame(AddGameToUserRequest request)
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user-id");

        var authenticatedUserId = Guid.Parse(userIdClaim!.Value);

        var roleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "role");
        var role = roleClaim?.Value;

        return await this.MakeSafeCallAsync(() => userService.AddGame(request, authenticatedUserId, role!));
    }
}
