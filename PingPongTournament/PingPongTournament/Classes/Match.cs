using System;
using System.Collections.Generic;
using System.Text;
using PingPongTournament.Interfaces;

namespace PingPongTournament.Classes
{
    public class Match
    {
        public int MatchNumber { get; set; }
        public IPlayable PlayerOne { get; set; }
        public IPlayable PlayerTwo { get; set; }
        public IPlayable MatchWinner { get; set; }
        public bool IsBye { get; set; } = false;

        public Match() { }
        public Match(IPlayable player1, IPlayable player2, int MatchNumber)
        {
            PlayerOne = player1;
            PlayerTwo = player2;
            this.MatchNumber = MatchNumber;
        }
        public Match(IPlayable player1, int MatchNumber, bool bye)
        {
            PlayerOne = player1;
            MatchWinner = PlayerOne;
            this.MatchNumber = MatchNumber;
            IsBye = bye;
        }
        public Match(IPlayable player1, IPlayable player2)
        {
            PlayerOne = player1;
            PlayerTwo = player2;
        }
    }
}
