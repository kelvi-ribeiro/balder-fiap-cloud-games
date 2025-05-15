using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Balder.FiapCloudGames.Api.Controllers;

[ApiController]
[Route("api/v1/games")]
public class GameController(IGameService gameService) : BaseController
{
    [HttpGet]
    [SwaggerOperation(Summary = "Visualizar todos Games", Description = "Endpoint para visualizar todos os Games já cadastrados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetAllGames()
    {
        return await this.MakeSafeCallAsync(gameService.GetAllGames);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Visualizar games", Description = "Endpoint para buscar o game baseado no ID pré-definido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.GetGameById(id));
    }

    [HttpGet("name/{name}")]
    [SwaggerOperation(Summary = "Visualizar games por nome", Description = "Endpoint para buscar o game baseado no Nome do Game")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetGameByName(string name)
    {
        return await this.MakeSafeCallAsync(() => gameService.GetGameByName(name));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Criar novo game", Description = "Endpoint para criar um novo game")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateGame([FromBody] GameRequest game)
    {
        return await this.MakeSafeCallAsync(() => gameService.CreateGame(game));
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Atualizar game", Description = "Endpoint para atualizar um game por ID")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateGame([FromBody] GameRequest game, [FromRoute] Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.UpdateGame(game, id));
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Deletar game", Description = "Endpoint para deletar game por ID pré-definido")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        return await this.MakeSafeCallAsync(() => gameService.DeleteGame(id));
    }
}
