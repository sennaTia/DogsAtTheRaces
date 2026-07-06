using System;
using System.Windows.Forms;

namespace DogsAtTheRaces
{
    public partial class BettingParlor : Form
    {
        private int joeCash = 50;
        private int bobCash = 75;
        private int alCash = 45;

        private int joeCurrentBet = 0;
        private int bobCurrentBet = 0;
        private int alCurrentBet = 0;

        private int joeDog = 0;
        private int bobDog = 0;
        private int alDog = 0;

        private Dog[] dogs = new Dog[4];

        public BettingParlor()
        {
            InitializeComponent();
            SetupLabels();
            SetupDogs();
        }

        private void SetupLabels()
        {
            rb_Guy1.Text = $"Joe has {joeCash} bucks";
            rb_Guy2.Text = $"Bob has {bobCash} bucks";
            rb_Guy3.Text = $"Al has {alCash} bucks";

            lb_guy1BetLabel.Text = "Joe hasn't placed a bet";
            lb_guy2BetLabel.Text = "Bob hasn't placed a bet";
            lb_guy3BetLabel.Text = "Al hasn't placed a bet";

            lb_minimumBet.Text = "5 bucks";
            lb_name.Text = "Joe";
            rb_Guy1.Checked = true;
        }

        private void SetupDogs()
        {
            dogs[0] = new Dog() { MyPictureBox = pb_dog1, StartingPosition = pb_dog1.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog1.Width };
            dogs[1] = new Dog() { MyPictureBox = pb_dog2, StartingPosition = pb_dog2.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog2.Width };
            dogs[2] = new Dog() { MyPictureBox = pb_dog3, StartingPosition = pb_dog3.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog3.Width };
            dogs[3] = new Dog() { MyPictureBox = pb_dog4, StartingPosition = pb_dog4.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog4.Width };
        }

        private void rb_Guy1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Guy1.Checked) lb_name.Text = "Joe";
        }

        private void rb_Guy2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Guy2.Checked) lb_name.Text = "Bob";
        }

        private void rb_Guy3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Guy3.Checked) lb_name.Text = "Al";
        }

        private void bt_bet_Click(object sender, EventArgs e)
        {
            int betAmount = (int)num_betAmount.Value;
            int dogNumber = (int)num_dogNumber.Value;

            string name = "";
            ref int cash = ref joeCash;
            ref int currentBet = ref joeCurrentBet;
            ref int dog = ref joeDog;
            RadioButton rb = rb_Guy1;
            Label lb = lb_guy1BetLabel;

            if (rb_Guy2.Checked) { name = "Bob"; cash = ref bobCash; currentBet = ref bobCurrentBet; dog = ref bobDog; rb = rb_Guy2; lb = lb_guy2BetLabel; }
            else if (rb_Guy3.Checked) { name = "Al"; cash = ref alCash; currentBet = ref alCurrentBet; dog = ref alDog; rb = rb_Guy3; lb = lb_guy3BetLabel; }
            else { name = "Joe"; } // Standaard Joe als Guy1 checked is

            int tempCash = cash + currentBet;
            if (tempCash >= betAmount)
            {
                cash = tempCash - betAmount;
                currentBet = betAmount;
                dog = dogNumber;

                rb.Text = $"{name} has {cash} bucks";
                lb.Text = $"{name} bets {betAmount} bucks on dog #{dogNumber}";
            }
            else { MessageBox.Show($"{name} has not enough money!"); }
        }

        private void bt_race_Click(object sender, EventArgs e)
        {
            gB_bettingParlor.Enabled = false;
            t_raceTimer.Start();
        }

        private void t_raceTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dogs.Length; i++)
            {
                if (dogs[i].Run())
                {
                    t_raceTimer.Stop();
                    int winningDog = i + 1;
                    MessageBox.Show($"Dog #{winningDog} won the race!");

                    // Uitbetaling
                    if (joeDog == winningDog) { joeCash += joeCurrentBet * 2; }
                    if (bobDog == winningDog) { bobCash += bobCurrentBet * 2; }
                    if (alDog == winningDog) { alCash += alCurrentBet * 2; }

                    // Reset
                    joeCurrentBet = 0; joeDog = 0;
                    bobCurrentBet = 0; bobDog = 0;
                    alCurrentBet = 0; alDog = 0;

                    foreach (Dog dog in dogs)
                    {
                        dog.TakeStartingPosition();
                    }

                    SetupLabels();
                    gB_bettingParlor.Enabled = true;
                    break;
                }
            }
        }
    }
}