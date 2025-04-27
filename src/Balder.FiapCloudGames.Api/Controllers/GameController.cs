using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/games")]
public class GameController(IGameService gameService) : BaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllGames()
    {
        return await this.MakeSafeCallAsync(gameService.GetAllGames);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.GetGameById(id));
    }

    [HttpGet("name/{name}")]
    [Authorize]
    public async Task<IActionResult> GetGameByName(string name)
    {
        return await this.MakeSafeCallAsync(() => gameService.GetGameByName(name));
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateGame([FromBody] GameRequest game)
    {
        return await this.MakeSafeCallAsync(() => gameService.CreateGame(game));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateGame([FromBody] GameRequest game, [FromRoute] Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.UpdateGame(game, id));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.DeleteGame(id));
    }
}
