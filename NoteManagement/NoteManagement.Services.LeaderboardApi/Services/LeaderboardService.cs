using NoteManagement.Services.LeaderboardApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace NoteManagement.Services.LeaderboardApi.Services
{
    public class LeaderboardService:ILeaderBoardService

    {
        private readonly HttpClient _httpclient;
        public LeaderboardService(HttpClient httpClient) {
            _httpclient = httpClient;
        }
        public async Task<List<Leaderboard>> GetLeaderboards()
        {
            var UserLink = "https://localhost:5007/api/User";
            var StreakLink = "https://localhost:5006/api/Streak";
            var users=await _httpclient.GetAsync(UserLink);
            var streak=await _httpclient.GetAsync(StreakLink);
            if(users.IsSuccessStatusCode && streak.IsSuccessStatusCode)
            {
                var response =await users.Content.ReadAsStringAsync();
                var UserList = JsonSerializer.Deserialize<List<User>>(response);

                var resp= await streak.Content.ReadAsStringAsync();
                var StreakList= JsonSerializer.Deserialize<List<Streak>>(resp);

                List<Leaderboard> leaderboards = new List<Leaderboard>();
                foreach(User u in UserList)
                {
                    var st = StreakList.FirstOrDefault(x=>x.identityUserId==u.identityUserId);
                    if(st==null)
                    {
                        continue;
                    }
                    leaderboards.Add(
                        new Leaderboard
                        {
                            Name = u.displayName,
                            Streak = st.streaks
                        }
                        );

                }
                List<Leaderboard> sortedli=leaderboards.OrderByDescending(x=>x.Streak).ToList();
                return sortedli;

            }
            return new List<Leaderboard>();
        }
    }
}
