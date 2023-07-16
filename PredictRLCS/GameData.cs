using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PredictRLCS
{
    internal class GameData
    {
        public struct Match
        {
            [JsonPropertyName("_id")] public string Id { get; set; }
            [JsonPropertyName("slug")] public string Slug { get; set; }
            [JsonPropertyName("octane_id")] public string OctaneId { get; set; }
            [JsonPropertyName("event")] public Event Event { get; set; }
            [JsonPropertyName("stage")] public Stage Stage { get; set; }
            [JsonPropertyName("date")] public string Date { get; set; }
            [JsonPropertyName("format")] public Format Format { get; set; }
            [JsonPropertyName("blue")] public Team Blue { get; set; }
            [JsonPropertyName("orange")] public Team Orange { get; set; }
            [JsonPropertyName("number")] public int Number { get; set; }
            [JsonPropertyName("games")] public List<Game> Games { get; set; }
        }

        public struct Event
        {
            [JsonPropertyName("_id")] public string Id { get; set; }
            [JsonPropertyName("slug")] public string Slug { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("region")] public string Region { get; set; }
            [JsonPropertyName("mode")] public int Mode { get; set; }
            [JsonPropertyName("tier")] public string Tier { get; set; }
            [JsonPropertyName("image")] public string Image { get; set; }
            [JsonPropertyName("groups")] public List<string> Groups { get; set; }
        }

        public struct Stage
        {
            [JsonPropertyName("_id")] public int Id { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("format")] public string Format { get; set; }
        }

        public struct Format
        {
            [JsonPropertyName("type")] public string Type { get; set; }
            [JsonPropertyName("length")] public int Length { get; set; }
        }

        public struct Team
        {
            [JsonPropertyName("team")] public TeamData Data { get; set; }
            [JsonPropertyName("score")] public int Score { get; set; }
            [JsonPropertyName("winner")] public bool Winner { get; set; }
            [JsonPropertyName("players")] public List<Player> Players { get; set; }
        }

        public struct TeamData
        {
            [JsonPropertyName("team")] public TeamInfo Info { get; set; }
            [JsonPropertyName("stats")] public Stats Stats { get; set; }
        }

        public struct TeamInfo
        {
            [JsonPropertyName("_id")] public string Id { get; set; }
            [JsonPropertyName("slug")] public string Slug { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("image")] public string Image { get; set; }
        }

        public struct Player
        {
            [JsonPropertyName("player")] public PlayerInfo Info { get; set; }
            [JsonPropertyName("stats")] public Stats Stats { get; set; }
            [JsonPropertyName("advanced")] public Advanced Advanced { get; set; }
        }

        public struct PlayerInfo
        {
            [JsonPropertyName("_id")] public string Id { get; set; }
            [JsonPropertyName("slug")] public string Slug { get; set; }
            [JsonPropertyName("tag")] public string Tag { get; set; }
            [JsonPropertyName("country")] public string Country { get; set; }
        }

        public struct Game
        {
            [JsonPropertyName("_id")] public string Id { get; set; }
            [JsonPropertyName("blue")] public int Blue { get; set; }
            [JsonPropertyName("orange")] public int Orange { get; set; }
            [JsonPropertyName("duration")] public int Duration { get; set; }
            [JsonPropertyName("ballchasing")] public string Ballchasing { get; set; }
        }

        public struct Stats
        {
            [JsonPropertyName("core")] public StatsCore Core { get; set; }
            [JsonPropertyName("boost")] public StatsBoost Boost { get; set; }
            [JsonPropertyName("movement")] public StatsMovement Movement { get; set; }
            [JsonPropertyName("positioning")] public StatsPositioning Positioning { get; set; }
            [JsonPropertyName("demo")] public StatsDemo Demo { get; set; }
        }

        public struct StatsCore
        {
            [JsonPropertyName("shots")] public int Shots { get; set; }
            [JsonPropertyName("goals")] public int Goals { get; set; }
            [JsonPropertyName("saves")] public int Saves { get; set; }
            [JsonPropertyName("assists")] public int Assists { get; set; }
            [JsonPropertyName("score")] public int Score { get; set; }
            [JsonPropertyName("shootingPercentage")] public float ShootingPercentage { get; set; }
        }

        public struct StatsBoost
        {
            [JsonPropertyName("bpm")] public float BPM { get; set; }
            [JsonPropertyName("bcpm")] public float BCPM { get; set; }
            [JsonPropertyName("avgAmount")] public float AvgAmount { get; set; }
            [JsonPropertyName("amountCollected")] public float AmountCollected { get; set; }
            [JsonPropertyName("amountStolen")] public float AmountStolen { get; set; }
            [JsonPropertyName("amountCollectedBig")] public int AmountCollectedBig { get; set; }
            [JsonPropertyName("amountStolenBig")] public int AmountStolenBig { get; set; }
            [JsonPropertyName("amountCollectedSmall")] public int AmountCollectedSmall { get; set; }
            [JsonPropertyName("amountStolenSmall")] public int AmountStolenSmall { get; set; }
            [JsonPropertyName("countCollectedBig")] public int CountCollectedBig { get; set; }
            [JsonPropertyName("countStolenBig")] public int CountStolenBig { get; set; }
            [JsonPropertyName("countCollectedSmall")] public int CountCollectedSmall { get; set; }
            [JsonPropertyName("countStolenSmall")] public int CountStolenSmall { get; set; }
            [JsonPropertyName("amountOverfill")] public int AmountOverfill { get; set; }
            [JsonPropertyName("amountOverfillStolen")] public int AmountOverfillStolen { get; set; }
            [JsonPropertyName("amountUsedWhileSupersonic")] public int AmountUsedWhileSupersonic { get; set; }
            [JsonPropertyName("timeZeroBoost")] public float TimeZeroBoost { get; set; }
            [JsonPropertyName("timeFullBoost")] public float TimeFullBoost { get; set; }
            [JsonPropertyName("timeBoost0To25")] public float TimeBoost0To25 { get; set; }
            [JsonPropertyName("timeBoost25To50")] public float TimeBoost25To50 { get; set; }
            [JsonPropertyName("timeBoost50To75")] public float TimeBoost50To75 { get; set; }
            [JsonPropertyName("timeBoost75To100")] public float TimeBoost75To100 { get; set; }
        }

        public struct StatsMovement
        {
            [JsonPropertyName("totalDistance")] public float TotalDistance { get; set; }
            [JsonPropertyName("timeSupersonicSpeed")] public float TimeSupersonicSpeed { get; set; }
            [JsonPropertyName("timeBoostSpeed")] public float TimeBoostSpeed { get; set; }
            [JsonPropertyName("timeSlowSpeed")] public float TimeSlowSpeed { get; set; }
            [JsonPropertyName("timeGround")] public float TimeGround { get; set; }
            [JsonPropertyName("timeLowAir")] public float TimeLowAir { get; set; }
            [JsonPropertyName("timeHighAir")] public float TimeHighAir { get; set; }
            [JsonPropertyName("timePowerslide")] public float TimePowerslide { get; set; }
            [JsonPropertyName("countPowerslide")] public float CountPowerslide { get; set; }
        }

        public struct StatsPositioning
        {
            [JsonPropertyName("timeDefensiveThird")] public float TimeDefensiveThird { get; set; }
            [JsonPropertyName("timeNeutralThird")] public float TimeNeutralThird { get; set; }
            [JsonPropertyName("timeOffensiveThird")] public float TimeOffensiveThird { get; set; }
            [JsonPropertyName("timeDefensiveHalf")] public float TimeDefensiveHalf { get; set; }
            [JsonPropertyName("timeOffensiveHalf")] public float TimeOffensiveHalf { get; set; }
            [JsonPropertyName("timeBehindBall")] public float TimeBehindBall { get; set; }
            [JsonPropertyName("timeInfrontBall")] public float TimeInfrontBall { get; set; }
        }

        public struct StatsDemo
        {
            [JsonPropertyName("inflicted")] public int Inflicted { get; set; }
            [JsonPropertyName("taken")] public int Taken { get; set; }
        }

        public struct Advanced
        {
            [JsonPropertyName("goalParticipation")] public float GoalParticipation { get; set; }
            [JsonPropertyName("rating")] public float Rating { get; set; }
        }
    }
}
