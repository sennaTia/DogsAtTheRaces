using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsAtTheRaces
{
  internal class Bet
    {
        public Guy Bettor;
        public int Dog;
        public int Amount;

        public int PayOut(int winner)
        {
            // als de hond wint, geef het bedrag terug
            if (Dog == winner) return Amount;

            // zo niet, geef het bedrag in de min terug
            return -Amount;
        }  
    }
}
