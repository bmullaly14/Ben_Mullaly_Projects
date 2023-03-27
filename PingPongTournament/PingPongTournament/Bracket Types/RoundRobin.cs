using PingPongTournament.Classes;
using PingPongTournament.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PingPongTournament.Bracket_Types
{
    public class RoundRobin : Bracket
    {
        public RoundRobin() { }
        private List<IPlayable> Players { get; set; }
        private List<IPlayable> DonePlayers { get; set; } = new List<IPlayable>();
        public List<IPlayable> mostWins { get; set; } = new List<IPlayable>();
        private int Games { get; set; }

        public override void Run(List<IPlayable> players)
        {
            Players = players;
            Games = Players.Count - 1;
            while (true)
            {
                
                Match currentMatch = MakeMatch();
                GetWinner(currentMatch);
                CleanPlayers();
                if (CheckProgress(Players))
                {
                    break;
                }
            }
            GetChampMatch(DonePlayers);
        }
        public void Run(List<IPlayable> players, bool clearWinners)
        {
            
            foreach(IPlayable p in players)
            {
                p.Wins = 0;
                p.Losses = 0;
                p.LostAgainst.Clear();
                p.WonAgainst.Clear();
                Players.Add(p);
            }
            Games = Players.Count - 1;
            if (clearWinners)
            {
                mostWins.Clear();
                DonePlayers.Clear();
            } else { DonePlayers.Clear(); }
            while (true)
            {

                Match currentMatch = MakeMatch();
                GetWinner(currentMatch);
                CleanPlayers();
                if (CheckProgress(Players))
                {
                    break;
                }
            }
            GetChampMatch(DonePlayers);
        }
        private Match MakeMatch()
        {
            Match m = new Match();
            Console.WriteLine("Please Select Player 1: ");
            ShowPlayers();
            int choice = GetUserInput(1, Players.Count);
            m.PlayerOne = Players[choice - 1];
            Console.WriteLine("Please Select Player 2:");
            List<IPlayable> choosePlayers = ShowPlayers(choice - 1);
            m.PlayerTwo = choosePlayers[GetUserInput(1, choosePlayers.Count) - 1];
            return m;
        }
        private void ShowPlayers()
        {
            int j = 1;
            for(int i = 0; i < Players.Count; i++)
            {
                if (DonePlayers.Contains(Players[i]))
                {
                    continue;
                }
                
                else
                {
                    Console.WriteLine($"{j} : {Players[i].Name}");
                    j++;
                }
            }
        }
        private void CleanPlayers()
        {
            foreach(IPlayable p in Players)
            if (p.Wins + p.Losses == Games)
                {
                    DonePlayers.Add(p);
                    
                }
            if (DonePlayers.Count == 0)
            {
                return;
            }
            foreach(IPlayable p in DonePlayers)
            {
                if (Players.Contains(p))
                {
                    Players.Remove(p);
                }
            }
        }
        private List<IPlayable> ShowPlayers(int exclude)
        {
            List<IPlayable> newPlayers = new List<IPlayable>();
            int j = 1;
            for(int i = 0; i < Players.Count; i++)
            {
                if (i == exclude || Players[i].PlayedAgainst().Contains(Players[exclude]))
                {
                    continue;
                }
                else {  
                    Console.WriteLine($"{j} : {Players[i].Name}");
                    newPlayers.Add(Players[i]);
                    j++;
                }
            }
            return newPlayers;
        }
        private IPlayable GetWinner(Match m)
        {
            Console.WriteLine("Who won the match?");
            Console.WriteLine($"1: {m.PlayerOne.Name} or 2: {m.PlayerTwo.Name}");
            int winner = GetUserInput(1, 2);
            AssignWinLoss(m, winner);
            if(winner == 1)
            {
                return m.PlayerOne;
            } else { return m.PlayerTwo; }
        }
        private void AssignWinLoss(Match m, int winner)
        {
            if(winner == 1)
            {
                m.PlayerOne.Wins++;
                m.PlayerTwo.Losses++;
                m.PlayerOne.WonAgainst.Add(m.PlayerTwo);
                m.PlayerTwo.LostAgainst.Add(m.PlayerOne);
            } else if (winner == 2)
            {
                m.PlayerOne.Losses++;
                m.PlayerTwo.Wins++;
                m.PlayerOne.LostAgainst.Add(m.PlayerTwo);
                m.PlayerTwo.WonAgainst.Add(m.PlayerOne);
            }
        }
        private bool CheckProgress(List<IPlayable> players)
        {
            if (players.Count == 0)
            {
                return true;
            }
            else { return false; }
        }
        private void GetChampMatch(List<IPlayable> players)
        {
            List<IPlayable> secondMostWins = new List<IPlayable>();
            int wins = 1;
            foreach(IPlayable p in players)
            {
                if (p.Wins > wins)
                {
                    wins = p.Wins; //this will get the max # of wins
                }
            }
            
                for(int i = wins; i >= 1; i--)
                {
                    foreach (IPlayable p in players)
                    {
                    if (p.Wins == i && mostWins.Count == 0) //add the first place person/first tied for first
                    {
                        mostWins.Add(p);
                    }
                    else if (mostWins.Count != 0 && p.Wins == mostWins[0].Wins && !mostWins.Contains(p)) //add any other players tied for first
                    {
                        mostWins.Add(p);
                    }
                    else if (mostWins.Count == 1 && p.Wins == i) //add the second most wins if no ties for first
                    {
                        secondMostWins.Add(p);
                    }
                    }
                }
            if(mostWins.Count == 1)
            {
                if (secondMostWins.Count == 2)
                {
                    Console.WriteLine($"There was a tie for second between {secondMostWins[0]} and {secondMostWins[1]}!");
                    Console.WriteLine("Press any key to play match for second place!");
                    Console.ReadKey();
                    mostWins.Add(GetWinner(new Match(secondMostWins[0], secondMostWins[1])));
                } else if(secondMostWins.Count == 1)
                {
                    mostWins.Add(secondMostWins[0]);
                } else if(secondMostWins.Count > 2)
                {
                    Console.WriteLine("The following players tied for second: ");
                    foreach(IPlayable p in secondMostWins)
                    {
                        Console.WriteLine($"- {p.Name}");
                    }
                    Console.WriteLine("Press any key to play for second!");
                    SingleElimination secondPlace = new SingleElimination();
                    mostWins.Add(secondPlace.TieRun(secondMostWins));
                }
            } 
            if (mostWins.Count > 2)
            {
                Console.WriteLine("The follow players tied for first: ");
                foreach(IPlayable p in mostWins)
                {
                    Console.WriteLine($"- {p.Name}");
                }
                Console.WriteLine("Press any key to play for final two");
                Console.ReadKey();
                Run(mostWins, true);
            } else
            {
                GetChampion(mostWins);
            }
        }
        public void GetChampion(List<IPlayable> players)
        {
            IPlayable champ = players[0];
            if (players.Count > 1)
            {
                Console.Clear();
                Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
                Console.WriteLine("         Championship Match          ");
                Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
                Console.WriteLine($"\n{players[0].Name} vs. {players[1].Name}");
                champ = GetWinner(new Match(players[0], players[1]));
            }
            Console.Clear();
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("         ! Congratulations !         ");
            Console.WriteLine($"             ! {champ.Name} !            ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.ReadKey();
        }
        public int GetUserInput(int min, int max)
        {
            bool parsed = int.TryParse(Console.ReadLine(), out int userChoice);

            while (!parsed || userChoice < min || userChoice > max)
            {
                Console.WriteLine("Please try again!");
                parsed = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }
    }
}
