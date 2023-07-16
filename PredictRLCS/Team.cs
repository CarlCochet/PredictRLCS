using Glicko2;

namespace PredictRLCS
{
    internal class Team
    {
        private string Name { get; set; }
        public List<Player> Players { get; set; }
        private int Rating { get; set; }
        private int GamesPlayed { get; set; }
        private string Region { get; set; }

        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
            Rating = 1500;
            GamesPlayed = 0;
            Region = "";
        }

        public Player? FindPlayer(string name)
        {
            return Players.FirstOrDefault(p => p.Name == name);
        }

        public void UpdateRating()
        {
            var totalRating = Players.Sum(p => p.Rating);
            GamesPlayed++;
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

        protected bool Equals(Team other)
        {
            return Name == other.Name && Players.Equals(other.Players) && Rating == other.Rating && GamesPlayed == other.GamesPlayed && Region == other.Region;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Team)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Players, Rating, GamesPlayed, Region);
        }
    }
}
