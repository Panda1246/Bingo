using Bingo.Services;
using Microsoft.AspNetCore.Mvc;
using Bingo.Models.Dto;

namespace Bingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService) : Controller
    {
        [HttpPost("create")]
        public IActionResult CreateGame([FromBody] CreateGameRequestDto request)
        {
            gameService.CreateGame(request);
            return Ok("GameId created successfully.");
        }

        [HttpPost("join")]
        public IActionResult JoinGame([FromBody] JoinGameDto request)
        {
            try
            {
                var board = gameService.JoinGame(request).Result;
                return Ok(board);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("leave")]
        public IActionResult LeaveGame([FromBody] LeaveGameDto request)
        {
            try
            {
                gameService.LeaveGame(request);
                return Ok("Left game successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
