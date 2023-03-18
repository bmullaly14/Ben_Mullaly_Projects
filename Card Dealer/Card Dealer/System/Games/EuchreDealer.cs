using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Card_Dealer.System.Collections;
using Card_Dealer.System.Interfaces;

namespace Card_Dealer.System.Games
{
    public class EuchreDealer : IDealable
    {
        public List<Card> Deck { get; set; }
        private List<Player> Players { get; set; }
        private Player Kitty { get; set; }

        public EuchreDealer(string[,] deck)
        {
            Deck = MakeDeck(deck);
            Kitty = new Player("Trump", 5);
            Players = GetPlayers();
            Players.Add(Kitty);
            

        }

        public List<Card> MakeDeck(string[,] inDeck)
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < inDeck.Length / 3; i++)
            {
                if (inDeck[i, 1] == "9" || inDeck[i, 1] == "10" || inDeck[i, 1] == "Jack" || inDeck[i, 1] == "Queen" || inDeck[i, 1] == "King" || inDeck[i, 1] == "Ace")
                {
                    Card newCard = makeCard(inDeck[i, 1], $"{inDeck[i, 2]}s");
                    deck.Add(newCard);
                }
            }
            return deck;
        }
        private Card makeCard(string num, string suit)
        {
            Card card = new Card(num, suit);
            return card;
        }
        private Stack<Card> Shuffle(List<Card> deck)
        {
            SortedDictionary<int, Card> shuffleDeck = new SortedDictionary<int, Card>();
            Random randy = new Random();

            foreach (Card card in deck)
            {
                shuffleDeck[randy.Next()] = card;
            }
            Stack<Card> stackDeck = new Stack<Card>();
            foreach (KeyValuePair<int, Card> c in shuffleDeck)
            {
                stackDeck.Push(c.Value);
            }

            return stackDeck;
        }
        public void Deal()
        {
            Stack<Card> shuffleDeck = Shuffle(Deck);
            while (shuffleDeck.Count > 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Players[i].PlayerDeck.Add(shuffleDeck.Pop());
                }
                
            }
            while (shuffleDeck.Count > 0)
            {
                Players[4].PlayerDeck.Add(shuffleDeck.Pop());
            }
            ShowCardsByPlayer();
        }
        private List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            int n = 1;
            while(n < 5)
            {
                Here:
                Console.WriteLine($"Please enter player {n}'s name:");
                string? playerName = Console.ReadLine();
                while (playerName.Length < 1)
                {
                    Console.WriteLine("Please try that again...");
                    playerName = Console.ReadLine();
                }
                
                {
                    while (true)
                    {
                        Console.WriteLine($"Is \"{playerName}\" correct? (y/n)");
                        bool wasParsed = char.TryParse(Console.ReadLine(), out char isYorN);

                        if (wasParsed && isYorN == 'y')
                        {
                            break;
                        }
                        else if(isYorN == 'n')
                        {
                            goto Here;
                        }
                        else
                        {
                            Console.WriteLine("Please try again!");
                        }
                        
                    }
                }
                    Player newPlayer = new Player(playerName, n);
                    players.Add(newPlayer);
                    n++;
                playerName = "";
            }
                return players;
        }
        private void ShowCardsByPlayer()
        {
            for(int i = 0; i < 4; i++)
            {
                Console.Clear();
                Console.WriteLine($"{Players[i].PlayerNumber}: {Players[i].PlayerName} (press any key to show)");
                Console.ReadKey();
                Console.WriteLine();
                foreach (Card c in Players[i].PlayerDeck)
                {
                    Console.WriteLine($"{c.Number} of {c.Suit}");
                }
                Console.WriteLine("****************************************");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine("Press any key to flip the kitty!");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine($"{Players[4].PlayerDeck[0].Number} of {Players[4].PlayerDeck[0].Suit}");
        }
    }
}






