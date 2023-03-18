using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using Card_Dealer.System.Collections;
using Card_Dealer.System.Interfaces;

namespace Card_Dealer.System.Games;

class WarDealer : IDealable
{
    public List<Card> Deck { get; set; }
    public List<Player>? Players { get; set; }

    public WarDealer(string[,] deck)
    {
        Deck = MakeDeck(deck);
    }

    private List<Card> MakeDeck(string[,] inDeck)
    {
        List<Card> deck = new List<Card>();
        for (int i = 0; i < inDeck.Length / 3; i++)
        {
            Card newCard = makeCard(inDeck[i, 1], inDeck[i, 2]);
            deck.Add(newCard);
        }
        return deck;
    }
    private Stack<Card> Shuffle(List<Card> deck)
    {
        SortedDictionary<int, Card> shuffleDeck = new SortedDictionary<int, Card>();
        Random randy = new Random();

        foreach(Card card in deck)
        {
            shuffleDeck[randy.Next()] = card;
        }
        Stack<Card> stackDeck = new Stack<Card>();
        foreach(KeyValuePair<int, Card> c in shuffleDeck)
        {
            stackDeck.Push(c.Value);
        }

        return stackDeck;
    }
    public void Deal()
    {
        Stack<Card> shuffled = Shuffle(Deck);
        int numberOfPlayers = GetPlayers();
        
        int cardsPerPlayer = 52 / numberOfPlayers;
        int remainingCards = 52 - cardsPerPlayer * numberOfPlayers;
        int totalCardsDealt = cardsPerPlayer * numberOfPlayers;
        Players = MakePlayers(numberOfPlayers);

        while (shuffled.Count >= numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players[i].PlayerDeck.Add(shuffled.Pop());
            }
        }

        ShowCardsByPlayer();
    }

    private void ShowCardsByPlayer()
    {
        foreach(Player p in Players)
        {
            Console.Clear();
            Console.WriteLine($"Player {p.PlayerNumber} (press any key to show)");
            Console.ReadKey();
            Console.WriteLine();
            foreach(Card c in p.PlayerDeck)
            {
                Console.WriteLine($"{c.Number} of {c.Suit}");
            }
            Console.WriteLine("****************************************");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
    private int GetPlayers()
    {
        Console.WriteLine("How many Players? You can play with 2-4 Players");
        while (true)
        {
            string? userInt = Console.ReadLine();
            if (int.TryParse(userInt, out int result) && result >= 2 && result <= 4)
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Please provide a number between 2 and 4.");
            }
        }
    }
    private Card makeCard(string num, string suit)
    {
        Card card = new Card(num, $"{suit}s");
        return card;
    }
    private List<Player> MakePlayers(int num)
    {
        List<Player> Players = new List<Player>();
        for(int i=1; i<=num; i++)
        {
            Player newPlayer = new Player(i);
            Players.Add(newPlayer);
        }

        return Players;
    }
}
