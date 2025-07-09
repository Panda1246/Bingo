using Bingo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController(IBoardService boardService) : Controller
    {
        [HttpGet("board/{gameId}/{userId}")]
        public IActionResult GetBoardByGameIdAndUserId(Guid gameId, Guid userId)
        {
            try
            {
                var board = boardService.GetBoardByGameIdAndUserId(gameId, userId).Result;
                if (board == null)
                {
                    return NotFound("BoardId not found for the specified game and user.");
                }
                return Ok(board);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving the board: {ex.Message}");
            }
        }
    }
}
