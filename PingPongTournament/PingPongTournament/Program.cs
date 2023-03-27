using PingPongTournament.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using PingPongTournament.Bracket_Types;
using PingPongTournament.Interfaces;

namespace PingPongTournament
{
    internal class Program
    {
        public static Bracket Bracket { get; set; }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                ShowMainMenu();
                int input = GetUserInput(0, 2);
                //while (true)
                //{
                if (input == 1)
                {
                    input = SinglesMenu();
                    SelectFromSingles(input);
                    if(input == 0)
                    {
                        Console.ReadKey();
                        continue;
                    }
                    List<IPlayable> players = GetPlayers();
                    if (players.Count <= 3)
                    {
                        Bracket = new RoundRobin();
                    }
                    Bracket.Run(players);
                    
                }
                else if (input == 2)
                {
                    input = DoublesMenu();
                    SelectFromDoubles(input);
                    if (input == 0)
                    {
                        Console.ReadKey();
                        continue;
                    }
                    List<IPlayable> teams = GetTeams();
                    NameTeams(teams);
                    if(teams.Count <= 3)
                    {
                        Bracket = new RoundRobin();
                    }
                    Bracket.Run(teams);
                }


                else if (input == 0)
                {
                    Environment.Exit(0);
                }
            }
        }

        public static List<IPlayable> GetTeams() 
        {
            List<IPlayable> teams = new List<IPlayable>();
            int teamNum = 0;
            Console.WriteLine("How many teams total?");
            int.TryParse(Console.ReadLine(), out teamNum);

            List<string> playersStringNames = new List<string> { };
            Console.WriteLine("Please input player names. Enter team mates on separate, adjacent lines.");
            while (playersStringNames.Count < teamNum * 2)
            {
                playersStringNames.Add(Console.ReadLine());
            }
            for(int p = 0; p <= playersStringNames.Count / 2; p+=2)
            {
                Player newPlayerOne = new Player(playersStringNames[p]);
                Player newPlayerTwo = new Player(playersStringNames[p + 1]);
                Team team = new Team(newPlayerOne, newPlayerTwo);
                teams.Add(team);
                
            }
            return teams;
        }
        public static void NameTeams(List<IPlayable> teams)
        {
            Console.Clear();
            foreach(IPlayable team in teams)
            {
                string player1 = team.GetPlayers()[0];
                string player2 = team.GetPlayers()[1];
                while (true)
                { Here:
                Console.WriteLine($"Enter a team name for {player1} and {player2}'s team:");
                string teamName = Console.ReadLine();
                    if(teamName.Length < 1)
                    {
                        Console.WriteLine("Please enter a team name!");
                    } else
                    {
                        
                        while (true)
                        {   
                            Console.WriteLine($"Is {teamName} correct? y/n");
                            string answer = Console.ReadLine();
                            if(answer == "y")
                            {
                                team.Name = teamName;
                                break;
                            } else if ( answer == "n")
                            {
                                goto Here;
                            } else { Console.WriteLine("Please enter y/n only"); }
                        }
                        if(team.Name != "")
                        {
                            break;
                        }
                    }
                }
                Console.Clear();
            }
        }
        public static List<IPlayable> GetPlayers()
        {
            List<IPlayable> players = new List<IPlayable>();
            int playerNum = 0;
            Console.WriteLine("How many players total?");
            int.TryParse(Console.ReadLine(), out playerNum);

            List<string> playersStringNames = new List<string> { };
            Console.WriteLine("Please input player names");
            while (playersStringNames.Count < playerNum)
            {
                playersStringNames.Add(Console.ReadLine());
            }
            foreach (string p in playersStringNames)
            {
                Player newPlayer = new Player(p);
                players.Add(newPlayer);
            }
            return players;


        }
        public static int GetUserInput(int min, int max)
        {
            bool parsed = int.TryParse(Console.ReadLine(), out int userChoice);

            while(!parsed || userChoice < min || userChoice > max)
            {
                Console.WriteLine("Please try again!");
                parsed = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }    
        
        public static void ShowMainMenu()
        {
            Console.WriteLine("Welcome to the Ping Pong Tournament Tool!");
            Console.WriteLine("\nPlease make a selection.\n");
            Console.WriteLine("(1) Singles");
            Console.WriteLine("(2) Doubles");
            Console.WriteLine("(0) Exit");
           
                                
        }
        public static int SinglesMenu()
        {
            Console.WriteLine("(1) Singles - Single Elimination");
            Console.WriteLine("(2) Singles - Double Elimination");
            Console.WriteLine("(3) Singles - Round Robin");
            Console.WriteLine("(0) Exit");
            int choice = GetUserInput(0, 3);
            return choice;
        }
        public static int DoublesMenu()
        {
            Console.WriteLine("(1) Doubles - Single Elimination");
            Console.WriteLine("(2) Doubles - Double Elimination");
            Console.WriteLine("(3) Doubles - Round Robin");
            Console.WriteLine("(0) Exit");
            int choice = GetUserInput(0, 3);
            return choice;
        }
        public static void SelectFromSingles(int i)
        {
            if (i == 1)
            {
                Bracket = new SingleElimination();
            }
            else if (i == 2)
            {
                Bracket = new DoubleElimination();
            } else if (i == 3)
            {
               Bracket = new RoundRobin();
            }
            else if (i == 0)
            {
                Console.WriteLine("Press any key to return to main menu.");
            }
        }
        public static void SelectFromDoubles(int i)
        {
            if (i == 1)
            {
                Bracket = new SingleElimination();
            }
            else if (i == 2)
            {
                Bracket = new DoubleElimination();
            }
            else if (i == 3)
            { 
                Bracket = new RoundRobin();
            } else if (i == 0)
            {
                Console.WriteLine("Press any key to return to main menu.");
            }
        }
    }
}
