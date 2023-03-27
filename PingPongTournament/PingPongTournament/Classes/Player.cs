using PingPongTournament.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PingPongTournament.Classes
{
    public class Player : IPlayable
    {
        public string Name { get; set; } 
        public int SeedNum { get; set; }
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public List<IPlayable> WonAgainst { get; set; } = new List<IPlayable>();
        public List<IPlayable> LostAgainst { get; set; } = new List<IPlayable>();

        public Player() { }
        public Player(string name)
        {
            Name = name;
        }
        public int WinGame(Player op)
        {
            WonAgainst.Add(op);
            Wins++;            
            return Wins;
        }
        public int LoseGame(Player op)
        {
            LostAgainst.Add(op);
            Losses++;
            return Losses;
        }
        public int GetRecord()
        {
            Console.WriteLine($"{Name}'s total wins: {Wins} -- total losses: {Losses}");
            Console.WriteLine($"{Name} has won against: ");
            foreach (Player p in WonAgainst)
            {
                Console.WriteLine(p.Name);
            }
            Console.WriteLine($"{Name} has lost against: ");
            foreach (Player p in LostAgainst)
            {
                Console.WriteLine(p.Name);
            }
            return Wins;
        }
        public List<string> GetPlayers()
        {
            List<string> player = new List<string>();
            player.Add(Name);
            return player;
        }
        public List<IPlayable> PlayedAgainst()
        {
            List<IPlayable> opponents = new List<IPlayable>();
            foreach(IPlayable p in WonAgainst)
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
