using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStats
{
    public class Stats
    {
        public string Name { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Kda { get; set; }
        public float WinPercentage
        {
            get
            {
                if (Wins > 0 && Losses > 0)
                {
                   float totalGames = Wins + Losses;
                    return (float)Wins / totalGames * 100;
                }
                return 100;
            }
        }
        public int LeaguePoints { get; set; }
        public GameTeam Team { get; set; }
        public enum GameTeam
        {
            Blue, Red, Unknown
        };
        public string Champion { get; set; }

        public override string ToString() => Name + ": " + Division + " with " + Wins + " wins and " + Losses + " losses." + Team;
    }
}
