using Balder.FiapCloudGames.Application.DTOs.Request;
using Balder.FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users =  await _userService.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userService.CreateUser(user);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userService.UpdateUser(user);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
