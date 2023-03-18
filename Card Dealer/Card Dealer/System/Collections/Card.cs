using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Card_Dealer.System.Interfaces;

namespace Card_Dealer.System.Collections
{
    public class Card : IDealable
    {
        
        public int Player { get; set; }
        public string Number { get; private set; }
        public string Suit { get; private set; }
        public bool IsDealt { get; set; } = false;

        public Card(string num, string suit)
        {
            Number = num;
            Suit = suit;
        }

        public void Deal()
        {
            if (IsDealt == false)
            {
                IsDealt = true;
            }
            else { return; }
        }
        public Card AssignPlayer(Card card, int player)
        {
            card.Player = player;
            return card;
        }
    }
}
