using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameService.GetAllGames();
            return Ok(games);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null)
                return NotFound($"Jogo não encontrado!");
            return Ok(game);
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetGameByName(string name)
        {
            var game = await _gameService.GetGameByName(name);
            if (game == null)
                return NotFound($"Jogo não encontrado!");
            return Ok(game);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameRequest game)
        {
            if (game == null)
                return BadRequest("Dados inválidos!");
            await _gameService.CreateGame(game);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] GameRequest game)
        {
            if (game == null)
                return BadRequest("Dados inválidos!");
            await _gameService.UpdateGame(game);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null)
                return NotFound($"Jogo não encontrado!");
            await _gameService.DeleteGame(id);
            return NoContent();
        }
    }
}
