using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService userService) : BaseController
{
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id)
    {
       return await this.MakeSafeCallAsync(() => userService.GetUserById(id));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        return await this.MakeSafeCallAsync(userService.GetAllUsers);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserRequest user, Guid id)
    {
       return await this.MakeSafeCallAsync(() => userService.UpdateUser(user, id));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        return await this.MakeSafeCallAsync(() => userService.DeleteUser(id));
    }
}
