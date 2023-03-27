using PingPongTournament.Classes;
using PingPongTournament.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace PingPongTournament.Bracket_Types
{
    public class DoubleElimination : Bracket
    {
        public IPlayable PlayerType { get; set; }
        public List<IPlayable> competitors;
        public List<IPlayable> losers = new List<IPlayable>();
        
        public DoubleElimination(int type)
        {
            BracketType = type;
   
        }
        public DoubleElimination() { }

        public override void Run(List<IPlayable> players)
        {
            Match final = new Match();
            MakeBracket(RandomizePlayers(players));
            AnnounceBracket(Matches);
            while (true)
            {
                if (Matches.Count > 1)
                {

                    competitors = new List<IPlayable>();
                    foreach (Match m in Matches)
                    {
                        IPlayable winner = AdvanceRound(m);
                        competitors.Add(winner);
                        losers.Add(GetMatchLoser(m, winner));
                    }
                    if (competitors.Count % 2 != 0)
                    {
                        players.Clear();
                        players = RandomizePlayers(competitors);
                        ClearMatches(Matches);
                        MakeBracket(players);
                    }
                    else
                    {
                        ClearMatches(Matches);
                        MakeBracket(competitors);
                    }
                    if (Matches.Count == 1)
                    {
                        break;
                    }
                    AnnounceBracket(Matches);

                }
                else 
                {
                    final = Matches[0];
                    break;
                }

            }
            
            ChampionshipMatch(Matches);
            IPlayable firstPlace = AdvanceRound(Matches[0]);
            IPlayable runnerUp = GetMatchLoser(Matches[0], firstPlace);
            IPlayable loserWinner = RunLoserBracket(losers);
            IPlayable secondPlace = PlayForSecond(loserWinner, runnerUp);
            IPlayable thirdPlace = GetMatchLoser(Matches[0], secondPlace);
            FinalStandings(firstPlace, secondPlace, thirdPlace);
            Console.ReadKey();
        }
        public IPlayable RunLoserBracket(List<IPlayable> players)
        {
            ClearMatches(Matches);       
            MakeBracket(RandomizePlayers(CleanPlayerList(players)));
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
            ChampionshipMatch(Matches);
            return AdvanceRound(Matches[0]);
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
        public IPlayable GetMatchLoser(Match m, IPlayable w)
        {
            if(m.PlayerOne == w)
            {
                return m.PlayerTwo;
            }
            return m.PlayerOne;
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
        public static List<IPlayable> CleanPlayerList(List<IPlayable> players)
        {
            List<IPlayable> outList = new List<IPlayable>();
            foreach(IPlayable p in players)
            {
                if(p == null)
                {
                    continue;
                } else
                {
                    outList.Add(p);
                }
            }
            return outList;
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
        public static void ChampionshipMatch(List<Match> m)
        {
            Console.Clear();
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("         Championship Match          ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine($"\n{m[0].PlayerOne.Name} vs. {m[0].PlayerTwo.Name}");
        }
        public static void FinalStandings(IPlayable first, IPlayable second, IPlayable third)
        {
            Console.Clear();
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("         ! Congratulations !         ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine($"First -- {first.Name}");
            Console.WriteLine($"Second -- {second.Name}");
            Console.WriteLine($"Third -- {third.Name}");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
        }
        public static void RunnerUpMatch(List<Match> m)
        {
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine("         Second Place Match          ");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*");
            Console.WriteLine($"\n{m[0].PlayerOne.Name} vs. {m[0].PlayerTwo.Name}");
        }
        public static void AnnounceBracket(List<Match> m)
        {
            Console.Clear();
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
        public IPlayable PlayForSecond(IPlayable one, IPlayable two)
        {
            List<IPlayable> players = new List<IPlayable>() { two, one };
            ClearMatches(Matches);
            MakeBracket(players);
            RunnerUpMatch(Matches);
            return AdvanceRound(Matches[0]);

        }
    }
}
