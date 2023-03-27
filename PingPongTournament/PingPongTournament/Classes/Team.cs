using PingPongTournament.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PingPongTournament.Classes
{
    public class Team : IPlayable
    {
        public string Name { get; set; }
        public int SeedNum { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public List<IPlayable> WonAgainst { get; set; } = new List<IPlayable>();
        public List<IPlayable> LostAgainst { get; set; } = new List<IPlayable>();
        public List<Player> Players { get; set; } = new List<Player>();

        public Team() { }
        public Team(Player player1, Player player2)
        {
            Players.Add(player1);
            Players.Add(player2);
        }
        public List<string> GetPlayers()
        {
            List<string> players = new List<string>();
            foreach(Player p in Players)
            {
                players.Add(p.Name);
            }
            return players;
        }
        public List<IPlayable> PlayedAgainst()
        {
            List<IPlayable> opponents = new List<IPlayable>();
            foreach (IPlayable p in WonAgainst)
            {
                opponents.Add(p);
            }
            foreach (IPlayable p in LostAgainst)
            {
                opponents.Add(p);
            }
            return opponents;
        }
    }
}
