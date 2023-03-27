using PingPongTournament.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using PingPongTournament.Interfaces;

namespace PingPongTournament.Bracket_Types
{
    public class SingleElimination : Bracket 
    {
        public SingleElimination() { }
        public override void Run(List<IPlayable> players)
        {
            MakeBracket(RandomizePlayers(players));
            AnnounceBracket(Matches);
            while (true)
            {
                if (Matches.Count > 1)
                {

                    List<IPlayable> tempList = new List<IPlayable>();
                    foreach (Match m in Matches)
                    {
                        tempList.Add(AdvanceRound(m));
                    }
                    if (tempList.Count % 2 != 0)
                    {
                        players.Clear();
                        players = RandomizePlayers(tempList);
                        ClearMatches(Matches);
                        MakeBracket(players);
                    }
                    else
                    {
                        ClearMatches(Matches);
                        MakeBracket(tempList);
                    }
                    if (Matches.Count == 1)
                    {
                        break;
                    }
                    AnnounceBracket(Matches);

                }
                else { break; }

            }
            Celebrate(FinalMatch(Matches));
        }
        public IPlayable TieRun(List<IPlayable> players)
        {
            MakeBracket(RandomizePlayers(players));
            AnnounceBracket(Matches);
            while (true)
            {
                if (Matches.Count > 1)
                {

                    List<IPlayable> tempList = new List<IPlayable>();
                    foreach (Match m in Matches)
                    {
                        tempList.Add(AdvanceRound(m));
                    }
                    if (tempList.Count % 2 != 0)
                    {
                        players.Clear();
                        players = RandomizePlayers(tempList);
                        ClearMatches(Matches);
                        MakeBracket(players);
                    }
                    else
                    {
                        ClearMatches(Matches);
                        MakeBracket(tempList);
                    }
                    if (Matches.Count == 1)
                    {
                        break;
                    }
                    AnnounceBracket(Matches);

                }
                else { break; }

            }
            return FinalMatch(Matches);
        }
        public static IPlayable SetMatchWinner(int matchID)
        {
            int matchIndex = Matches.IndexOf(GetMatchByNum(matchID));
            if (Matches[matchIndex].IsBye)
            {
                return Matches[matchIndex].MatchWinner;
            }
            Console.WriteLine($"Did {Matches[matchIndex].PlayerOne.Name} (1) or {Matches[matchIndex].PlayerTwo.Name} (2) win?");
            bool parsed = int.TryParse(Console.ReadLine(), out int userIn);
            bool rightNum = false;
            if (userIn == 1 || userIn == 2)
            {
                rightNum = true;
            }

            while (!parsed || !rightNum)
            {
                Console.WriteLine("Please try again!");
                parsed = int.TryParse(Console.ReadLine(), out userIn);
                if (userIn == 1 || userIn == 2)
                {
                    rightNum = true;
                }
            }
            if (userIn == 1)
            {
                Matches[matchIndex].MatchWinner = Matches[matchIndex].PlayerOne;

            }
            else if (userIn == 2) { Matches[matchIndex].MatchWinner = Matches[matchIndex].PlayerTwo; }
            return Matches[matchIndex].MatchWinner;
        }
        public static new void AddMatchToBracket(Match m)
        {
            Matches.Add(m);
        }
        public static Match GetMatchByNum(int matchID)
        {
            Match selectedMatch = null;
            foreach (Match m in Matches)
            {
                if (m.MatchNumber == matchID)
                {
                    selectedMatch = m;
                    break;
                }

            }
            return selectedMatch;
        }
        public IPlayable AdvanceRound(Match m)
        {
            IPlayable w = SetMatchWinner(m.MatchNumber);

            return w;
        }
        public static bool ClearMatches(List<Match> matches)
        {
            matches.Clear();
            return true;
        }

        public static List<IPlayable> RandomizePlayers(List<IPlayable> players)
        {
            Random rand = new Random();

            //Dictionary<int, string> bracketPositions = new Dictionary<int, string>();

            for (int i = 0; i < players.Count; i++)
            {
                players[i].SeedNum = rand.Next();
            }

            SortedDictionary<int, IPlayable> sortBrack = new SortedDictionary<int, IPlayable>();
            List<IPlayable> sortPlayers = new List<IPlayable>();
            foreach (IPlayable p in players)
            {
                sortBrack[p.SeedNum] = p;
            }

            foreach (KeyValuePair<int, IPlayable> name in sortBrack)
            {
                sortPlayers.Add(name.Value);
            }

            return sortPlayers;

        }
        public static void MakeBracket(List<IPlayable> sortplayers)
        {
            ClearMatches(Matches);
            int j = 1;

            {
                if (sortplayers.Count % 2 == 0! || sortplayers.Count == 2)
                {
                    for (int i = 0; i < sortplayers.Count; i += 2)
                    {

                        Match match = new Match(sortplayers[i], sortplayers[i + 1], j);
                        AddMatchToBracket(match);
                        j++;
                    }
                }
                else
                {
                    for (int i = 0; i < sortplayers.Count - 1; i += 2)
                    {
                        //Console.WriteLine($"Game {j} -- {sortplayers[i].Name} vs. {sortplayers[i + 1].Name}");
                        Match match = new Match(sortplayers[i], sortplayers[i + 1], j);
                        AddMatchToBracket(match);
                        j++;
                    }
                    //Console.WriteLine($"{sortplayers[sortplayers.Count - 1].Name} -- Bye ");
                    Match byeMatch = new Match(sortplayers[sortplayers.Count - 1], j, true);
                    AddMatchToBracket(byeMatch);
                }


            }
        }
        public static IPlayable FinalMatch(List<Match> m)
        {
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("             Final Match             ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine($"\n{m[0].PlayerOne.Name} vs. {m[0].PlayerTwo.Name}");
            return SetMatchWinner(m[0].MatchNumber);
        }
        public static void AnnounceBracket(List<Match> m)
        {
            Console.WriteLine("Here's the next round!");
            foreach (Match i in m)
            {
                if (i.IsBye)
                {
                    Console.WriteLine($"Bye -- {i.PlayerOne.Name}");
                }
                else
                {
                    Console.WriteLine($"Game {i.MatchNumber} -- {i.PlayerOne.Name} vs. {i.PlayerTwo.Name}");
                }
            }
        }
        public static void Celebrate(IPlayable champ)
        {
            Console.Clear();
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("         ! Congratulations !         ");
            Console.WriteLine($"             ! {champ.Name} !            ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.ReadKey();
        }
    }
}
//   