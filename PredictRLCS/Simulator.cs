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

            }

            Regions = regions;
        }
    }
}
