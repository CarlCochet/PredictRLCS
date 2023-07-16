using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictRLCS
{
    internal class Player
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public int GamesPlayed { get; set; }

        public Player(string name)
        {
            Name = name;
            Rating = 1500;
            GamesPlayed = 0;
        }

        public void UpdateRating(double change, int score, List<int> teamScores)
        {
            var gamma = 6.0;
            var teamScore = (double)(teamScores.Sum());
            var scoreRatio = score / teamScore;

            if (change > 0)
            {
                var sa = 3.0 * (Math.Pow(scoreRatio, gamma) / (
                            (Math.Pow(teamScores[0] / teamScore, gamma)) +
                            (Math.Pow(teamScores[1] / teamScore, gamma)) +
                            (Math.Pow(teamScores[2] / teamScore, gamma))
                        )
                    );
                Rating += (int)(change * sa);
            }
            else
            {
                var sa = 3.0 * (Math.Pow(1.0 / scoreRatio, gamma) / (
                            (Math.Pow(1.0 / (teamScores[0] / teamScore), gamma)) +
                            (Math.Pow(1.0 / (teamScores[1] / teamScore), gamma)) +
                            (Math.Pow(1.0 / (teamScores[2] / teamScore), gamma))
                        )
                    );
                Rating += (int)(Math.Abs(change) * sa);
            }

            GamesPlayed++;
        }
    }
}
