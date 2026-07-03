namespace DogsAtTheRaces;

public partial class BettingParlor : Form
{
    private readonly Dog[] _racingDogs = new Dog[4];
    private readonly Guy[] _players = new Guy[3];
    private bool _isRaceActive;

    public BettingParlor()
    {
        InitializeComponent();

        _players[0] = new Guy { Name = "Joe", Cash = 50, MyRadioButton = rb_Guy1, MyLabel = lb_guy1BetLabel };
        _players[1] = new Guy { Name = "Bob", Cash = 75, MyRadioButton = rb_Guy2, MyLabel = lb_guy2BetLabel };
        _players[2] = new Guy { Name = "Al", Cash = 45, MyRadioButton = rb_Guy3, MyLabel = lb_guy3BetLabel };

        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].UpdateLabels();
        }

        _racingDogs[0] = new Dog { MyPictureBox = pb_dog1, StartingPosition = pb_dog1.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog1.Width };
        _racingDogs[1] = new Dog { MyPictureBox = pb_dog2, StartingPosition = pb_dog2.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog2.Width };
        _racingDogs[2] = new Dog { MyPictureBox = pb_dog3, StartingPosition = pb_dog3.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog3.Width };
        _racingDogs[3] = new Dog { MyPictureBox = pb_dog4, StartingPosition = pb_dog4.Left, RaceTrackLength = pb_raceTrack.Width - pb_dog4.Width };
    }

    private void bt_bet_Click(object sender, EventArgs e)
    {
        if (_isRaceActive)
        {
            MessageBox.Show("You can't bet while the race is running!");
            return;
        }

        Guy activeGuy = rb_Guy1.Checked ? _players[0] : (rb_Guy2.Checked ? _players[1] : (rb_Guy3.Checked ? _players[2] : null));

        if (activeGuy != null)
        {
            activeGuy.PlaceBet(5, (int)num_dogNumber.Value);
            activeGuy.UpdateLabels();
        }
    }

    private void bt_race_Click(object sender, EventArgs e)
    {
        if (!_isRaceActive)
        {
            _isRaceActive = true;

            for (int i = 0; i < _racingDogs.Length; i++)
            {
                _racingDogs[i].TakeStartingPosition();
            }

            t_raceTimer.Start();
        }
    }

    private void t_raceTimer_Tick(object sender, EventArgs e)
    {
        for (int index = 0; index < _racingDogs.Length; index++)
        {
            if (_racingDogs[index].Run())
            {
                t_raceTimer.Stop();
                _isRaceActive = false;

                int winnerId = index + 1;

                foreach (Guy player in _players)
                {
                    player.Collect(winnerId);
                    player.ClearBet();
                    player.UpdateLabels();
                }

                foreach (Dog hound in _racingDogs)
                {
                    hound.TakeStartingPosition();
                }

                MessageBox.Show($"Dog {winnerId} wins!");
                break;
            }
        }
    }
}