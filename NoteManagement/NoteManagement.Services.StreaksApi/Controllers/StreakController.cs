using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.StreaksApi.Models;
using NoteManagement.Services.StreaksApi.Services;
using System.Threading.Tasks;

namespace NoteManagement.Services.StreaksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreakController : ControllerBase
    {
        private readonly IStreakService _streakService;

        public StreakController(IStreakService streakService)
        {
            _streakService = streakService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allStreaks = await _streakService.Getall();
            return Ok(allStreaks);
        }

     

        [HttpGet("ById")]
        public async Task<IActionResult> GetById()
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault().Trim();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            Console.WriteLine(userid);
            var streak = await _streakService.GetStreakById(userid);
            Console.WriteLine(streak);
            if (streak == null)
            {
                return NotFound("Streak not found for the given user ID.");
            }
            return Ok(streak);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostUser(Streak obj)
        {
            bool res = await _streakService.CreateStreak(obj);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("Increment/")]
        public async Task<IActionResult> Increment([FromBody] string id)
        {
            bool result = await _streakService.IncrementStreak(id);
            if (!result)
            {
                return NotFound("Could not increment streak. User ID may not exist.");
            }
            return Ok("Streak incremented successfully.");
        }

        [HttpPut("Reset/{id}")]
        public async Task<IActionResult> Reset(string id)
        {
            bool result = await _streakService.Reset(id);
            if (!result)
            {
                return NotFound("Could not reset streak. User ID may not exist.");
            }
            return Ok("Streak reset successfully.");
        }
    }
}


