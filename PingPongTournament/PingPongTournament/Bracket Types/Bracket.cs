using System;
using System.Collections.Generic;
using System.Text;
using PingPongTournament.Interfaces;

namespace PingPongTournament.Classes
{
    public class Bracket
    {
        public static List<Match> Matches { get; set; } = new List<Match>();
        public int BracketType { get; set; } = 0;
        public virtual void Run(List<IPlayable> players) { }
        //public virtual IPlayable Run(List<IPlayable> players) { }
        public static void AddMatchToBracket(Match m) { }
    }
}
