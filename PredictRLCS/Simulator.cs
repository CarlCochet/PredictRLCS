using Glicko2;

namespace PredictRLCS
{
    internal class Simulator
    {
        private List<GameData.Match> Matches { get; set; }
        private List<Region> Regions { get; set; }
        private List<Player> Players { get; set; }

        public Simulator(List<GameData.Match> matches)
        {
            Matches = matches;
            Regions = new List<Region>();
            Players = new List<Player>();
        }

        public void Simulate()
        {
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

                Team? blueTeam;
                Team? orangeTeam;
                
                if (match.Event.Region != "INT")
                {
                    var region = GetRegion(match.Event.Region);
                    region.FillTeam(blueTeamData.Data.Info.Name, bluePlayers, region.Name);
                    region.FillTeam(orangeTeamData.Data.Info.Name, orangePlayers, region.Name);
                    blueTeam = region.GetTeam(bluePlayers);
                    orangeTeam = region.GetTeam(orangePlayers);
                }
                else
                {
                    var blueRegion = FindRegion(blueTeamData.Data.Info.Region, bluePlayers);
                    var orangeRegion = FindRegion(orangeTeamData.Data.Info.Region, orangePlayers);
                    blueRegion.FillTeam(blueTeamData.Data.Info.Name, bluePlayers, blueRegion.Name);
                    orangeRegion.FillTeam(orangeTeamData.Data.Info.Name, orangePlayers, orangeRegion.Name);
                    blueTeam = blueRegion.GetTeam(bluePlayers);
                    orangeTeam = orangeRegion.GetTeam(orangePlayers);
                }

                if (blueTeam == null || orangeTeam == null) continue;
                SimulateMatch(blueTeam, orangeTeam, match);
            }
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
        
        private Region FindRegion(string? regionName, List<Player> players)
        {
            if (regionName == null)
            {
                foreach (var r in Regions)
                {
                    var team = r.GetTeam(players);
                    if (team != null) return r;
                }
                
            }
            
            var region = Regions.Find(r => r.Name == regionName);
            if (region != null) return region;
            region = new Region(regionName);
            Regions.Add(region);
            return region;
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
                    Players.Add(player);
                }
            }
            
            return players;
        }
    }
}
