using System;
using System.Collections.Generic;
using System.Text;

namespace PingPongTournament.Classes
{
    public class DoublesMatch : Match
    {
        public new int MatchNumber { get; set; }

        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }
        public new Team MatchWinner { get; set; }
        public bool IsBye { get; set; }

        public DoublesMatch(Player player1, Player player2, Player player3, Player player4, int MatchNumber) : base()
        {
            TeamOne = new Team(player1, player2);
            TeamTwo = new Team(player3, player4);
        }
    }
}
