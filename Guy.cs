using System;
using System.Windows.Forms;

namespace DogsAtTheRaces
{
    internal class Guy
    {
        public int Cash;
        public string Name = string.Empty;
        public Bet? MyBet;

        public RadioButton? MyRadioButton;
        public Label? MyLabel;

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
            if (MyBet == null) return;

            Cash += MyBet.PayOut(winner);
            MyBet = null;
        }

        public void UpdateLabels()
        {
            if (MyRadioButton != null)
            {
                MyRadioButton.Text = Name + " has " + Cash + " bucks";
            }

            if (MyLabel != null)
            {
                if (MyBet == null)
                {
                    MyLabel.Text = Name + " hasn't placed a bet";
                }
                else
                {
                    MyLabel.Text = Name + " bets " + MyBet.Amount + " bucks on dog #" + MyBet.Dog;
                }
            }
        }

        public void ClearBet()
        {
            MyBet = null;
        }
    }
}