using Glicko2;
using static PredictRLCS.GameData;

namespace PredictRLCS
{
    internal class Region
    {
        private string Name { get; set; }
        private List<Team> Teams { get; set; }
        private int Rating { get; set; }

        public Region(string name)
        {
            Name = name;
            Teams = new List<Team>();
            Rating = 1500;
        }

        public int GetTeamIndex(List<Player> players)
        {
            return Teams.FindIndex(t => players == t.Players);
        }

        public void FillTeam(string name, List<Player> players)
        {
            if (GetTeamIndex(players) == -1)
            {
                var team = new Team(name);
                team.Players = players;
                team.UpdateRating();
                Teams.Add(team);
            }
        }

        public Team? GetTeam(List<Player> players)
        { 
            return Teams.Find(t => players == t.Players);
        }

        public Player? GetPlayer(string name)
        {
            return Teams.SelectMany(t => t.Players).FirstOrDefault(p => p.Name == name);
        }
    }
}
