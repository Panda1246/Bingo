using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController(FieldController fieldController) : Controller
    {
        [HttpPost("mark")]
        public IActionResult MarkField([FromBody] Guid field)
        {
            try
            {
                return Ok(fieldController.MarkField(field));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Field cannot be null: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while marking the field: {ex.Message}");
            }
        }

        [HttpPost("unmark")]
        public IActionResult UnmarkField([FromBody] Guid field)
        {
            try
            {
                return Ok(fieldController.UnmarkField(field));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Field cannot be null: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while unmarking the field: {ex.Message}");
            }
        }
    }
}
