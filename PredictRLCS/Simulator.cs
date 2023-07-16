using Glicko2;

namespace PredictRLCS
{
    internal class Simulator
    {
        private List<GameData.Match> Matches { get; set; }
        private List<Region> Regions { get; set; }

        public Simulator(List<GameData.Match> matches)
        {
            Matches = matches;
            Regions = new List<Region>();
        }

        public void Simulate()
        {
            var regions = new List<Region>();

            foreach (var match in Matches)
            {
                var blueTeamData = match.Blue;
                var orangeTeamData = match.Orange;
                
                if(blueTeamData.Players.Count != 3 || orangeTeamData.Players.Count != 3)
                {
                    continue;
                }

                var bluePlayers = FindPlayers(blueTeamData);
                var orangePlayers = FindPlayers(orangeTeamData);

                var region = GetRegion(match.Event.Region);
                region.FillTeam(blueTeamData.Data.Info.Name, bluePlayers);
                region.FillTeam(orangeTeamData.Data.Info.Name, orangePlayers);
                
                var blueTeam = region.GetTeam(bluePlayers);
                var orangeTeam = region.GetTeam(orangePlayers);
                
                if (blueTeam == null || orangeTeam == null) continue;
                SimulateMatch(blueTeam, orangeTeam, match);
            }

            Regions = regions;
        }

        private void SimulateMatch(Team blueTeam, Team orangeTeam, GameData.Match match)
        {
            var blueScore = 0;
            var orangeScore = 0;

            var games = match.Games;
            
            foreach (var game in games)
            {
                var blueGameScore = game.Blue;
                var orangeGameScore = game.Orange;

                if (blueGameScore > orangeGameScore)
                {
                    blueScore++;
                }
                else if (orangeGameScore > blueGameScore)
                {
                    orangeScore++;
                }
            }
            
            var blueRating = blueTeam.GlickoParams.Rating;
            var orangeRating = orangeTeam.GlickoParams.Rating;

            var blueOpponent = new GlickoOpponent(orangeTeam.GlickoParams, Convert.ToInt32(blueScore > orangeScore));
            var orangeOpponent = new GlickoOpponent(blueTeam.GlickoParams, Convert.ToInt32(blueScore < orangeScore));
            
            blueTeam.GlickoParams = GlickoCalculator.CalculateRanking(blueTeam.GlickoParams, new List<GlickoOpponent>() { blueOpponent });
            orangeTeam.GlickoParams = GlickoCalculator.CalculateRanking(orangeTeam.GlickoParams, new List<GlickoOpponent>() { orangeOpponent });

            blueTeam.UpdatePlayersRatings(blueTeam.GlickoParams.Rating - blueRating, match.Blue);
            orangeTeam.UpdatePlayersRatings(orangeTeam.GlickoParams.Rating - orangeRating, match.Orange);
            
            
        }
        
        private Region GetRegion(string name)
        {
            var region = Regions.Find(r => r.Name == name);
            if (region != null) return region;
            region = new Region(name);
            Regions.Add(region);
            return region;
        }

        private List<Player> FindPlayers(GameData.Team team)
        {
            var playerNames = team.Players.Select(p => p.Info.Slug).ToList();
            var players = new List<Player>();
            
            foreach (var playerName in playerNames)
            {
                var found = false;
                foreach (var region in Regions)
                {
                    var player = region.GetPlayer(playerName);
                    if (player == null) continue;
                    players.Add(player);
                    found = true;
                }
                if (!found)
                {
                    var player = new Player(playerName);
                    players.Add(player);
                }
            }
            
            return players;
        }
    }
}
