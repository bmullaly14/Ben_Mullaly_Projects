using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Card_Dealer.System.Interfaces;

namespace Card_Dealer.System.Collections
{
    public class Deck : IDealable
    {
        public int Player { get; }
        public void Deal(Card card) 
        {
            card.Player = this.Player;
            card.Deal();
        }

    }
}
