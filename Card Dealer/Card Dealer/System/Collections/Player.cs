using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Card_Dealer.System.Collections
{
    public class Player
    {
        public List<Card> PlayerDeck { get; set; }
        public int PlayerNumber { get; set; }
        public string PlayerName { get; set; }

        public Player(int num)
        {
            PlayerNumber = num;
            PlayerDeck = new List<Card>();
        }
        public Player(string name, int num)
        {
            PlayerName = name;
            PlayerNumber = num;
            PlayerDeck = new List<Card>();
        }
    }
}
