using System;
using System.Collections.Generic;
using System.Text;
using PingPongTournament.Classes;

namespace PingPongTournament.Interfaces
{
    public interface IPlayable
    {
        string Name { get; set; } 
        int SeedNum { get; set; }
        int Wins { get; set; }
        int Losses { get; set; }
        List<IPlayable> WonAgainst { get; set; }
        List<IPlayable> LostAgainst { get; set; }
        public List<string> GetPlayers();
        public List<IPlayable> PlayedAgainst();
    }
}
