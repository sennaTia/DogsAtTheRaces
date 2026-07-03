using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsAtTheRaces
{
    internal class Guy
    {
        public int Cash;
        public string Name;
        public Bet MyBet;

        public bool PlaceBet(int amount, int dog)
        {
            if (amount <= Cash)
            {
                MyBet = new Bet();
                MyBet.Amount = amount;
                MyBet.Dog = dog;
                MyBet.Bettor = this; 

                return true;
            }

            return false;
        }

        public void Collect(int winner)
        {
            if (MyBet == null) return; // Stop meteen als er geen weddenschap is

            Cash += MyBet.PayOut(winner);
            MyBet = null; // stop en start de weddenschap opnieuw
        }
    }
}