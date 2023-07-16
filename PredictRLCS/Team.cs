using Glicko2;

namespace PredictRLCS
{
    internal class Team
    {
        private string Name { get; set; }
        public List<Player> Players { get; set; }
        public GlickoPlayer GlickoParams { get; set; }
        private int GamesPlayed { get; set; }
        private string Region { get; set; }

        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
            GlickoParams = new GlickoPlayer();
            GamesPlayed = 0;
            Region = "";
        }

        public Player? FindPlayer(string name)
        {
            return Players.FirstOrDefault(p => p.Name == name);
        }

        public void UpdateRating()
        {
            var totalRating = (double)Players.Sum(p => p.Rating);
            GlickoParams = new GlickoPlayer(totalRating / Players.Count);
            GamesPlayed++;
        }

        public void UpdatePlayersRatings(double change, GameData.Team team)
        {
            foreach (var playerData in team.Players)
            {
                var player = FindPlayer(playerData.Info.Slug);
                if (player == null) continue;
                var playersScores = team.Players.Select(p => p.Stats.Core.Score).ToList();
                player.UpdateRating(change, playerData.Stats.Core.Score, playersScores);
            }
        }

        public static bool operator ==(List<Player>? players, Team? team)
        {
            if (players == null || team == null)
            {
                return false;
            }

            return team.Players.All(player => players.Any(p => p.Name == player.Name));
        }

        public static bool operator !=(List<Player> players, Team team)
        {
            return !(players == team);
        }

        private bool Equals(Team other)
        {
            return Name == other.Name && Players.Equals(other.Players) && GlickoParams == other.GlickoParams && GamesPlayed == other.GamesPlayed && Region == other.Region;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Team)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Players, GlickoParams, GamesPlayed, Region);
        }
    }
}
