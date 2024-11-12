using NoteManagement.Services.LeaderboardApi.Models;

namespace NoteManagement.Services.LeaderboardApi.Services
{
    public interface ILeaderBoardService
    {
        public Task<List<Leaderboard>> GetLeaderboards();
    }
}
