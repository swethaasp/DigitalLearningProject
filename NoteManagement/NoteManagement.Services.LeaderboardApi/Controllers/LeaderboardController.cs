using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.LeaderboardApi.Services;

namespace NoteManagement.Services.LeaderboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderboardService;
        public LeaderboardController(ILeaderBoardService leaderBoard) {
            _leaderboardService = leaderBoard;
        }
        [HttpGet]
        public async Task<IActionResult> GetLeaderboard()
        {
            var response=await _leaderboardService.GetLeaderboards();
            return Ok(response);
        }
    }
}
