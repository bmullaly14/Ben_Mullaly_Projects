using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using Card_Dealer.System.Games;
using Card_Dealer.System.Interfaces;

namespace Card_Dealer.System;
class Program 
{
    public static void Main(string[] args)
    {
        
        Dictionary<int, string> deckList = new Dictionary<int, string>();
        Dictionary<int, string> euchreList = new Dictionary<int, string>();
        string[,] deckOfCards = { {"", "2", "Spade" }, {"","3", "Spade" }, {"", "4", "Spade" }, {"", "5", "Spade" }, {"", "6", "Spade" }, {"", "7", "Spade" }, {"", "8", "Spade" }, {"", "9", "Spade" }, {"", "10", "Spade" },
            {"", "Jack", "Spade" }, {"", "Queen", "Spade" }, {"", "King", "Spade" }, {"", "Ace","Spade" },{"", "2", "Club" }, {"", "3", "Club" }, {"", "4", "Club" }, {"", "5", "Club" }, {"", "6", "Club" }, {"", "7", "Club" }, {"", "8", "Club" },
            {"", "9", "Club" }, {"", "10", "Club" },{"", "Jack", "Club" }, {"", "Queen", "Club" }, {"", "King", "Club" }, {"", "Ace","Club" }, {"", "2", "Heart" }, {"", "3", "Heart" }, {"", "4", "Heart" }, {"", "5", "Heart" }, {"", "6", "Heart" },
            {"", "7", "Heart" }, {"", "8", "Heart" }, {"", "9", "Heart" }, {"", "10", "Heart" },{"", "Jack", "Heart" }, {"", "Queen", "Heart" }, {"", "King", "Heart" }, {"", "Ace","Heart"}, {"", "2", "Diamond" }, {"", "3", "Diamond" },
            {"", "4", "Diamond" }, {"", "5", "Diamond" }, {"", "6", "Diamond" }, {"", "7", "Diamond" }, {"", "8", "Diamond" }, {"", "9", "Diamond" }, {"", "10", "Diamond" },{"", "Jack", "Diamond" }, {"", "Queen", "Diamond" }, {"", "King", "Diamond" }, {"", "Ace","Diamond" } };
        
        int userInput;
        Console.WriteLine("What would you like to play?");
        Console.WriteLine("1) Euchre | 2) War");
        userInput = GetUserInt(1, 2);

        if (userInput == 1)
        {
            IDealable euchre = new EuchreDealer(deckOfCards);
            euchre.Deal();
        } else
        {
            IDealable war = new WarDealer(deckOfCards);
            war.Deal();
        }
        

    }   
    private static int GetUserInt(int min, int max)
    {
        
        while (true)
        {
            string? userInt = Console.ReadLine();
            if(int.TryParse(userInt, out int result) && result >= min && result <= max)
            {
                return result;
            } else
            {
                Console.WriteLine($"Please provide a number between {min} and {max}.");
            }
        }
    }
    
}


    
