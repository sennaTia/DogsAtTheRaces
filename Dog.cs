using System;
using System.Windows.Forms;

namespace DogsAtTheRaces
{
    internal class Dog
    {
        public PictureBox? MyPictureBox;
        public int Location;
        public int StartingPosition;
        public int RaceTrackLength;

        public static Random Randomizer = new Random();

        public bool Run()
        {
            Location += Randomizer.Next(1, 5);

            if (MyPictureBox != null)
            {
                MyPictureBox.Left = StartingPosition + Location;
                return (MyPictureBox.Left >= RaceTrackLength);
            }

            return false;
        }

        public void TakeStartingPosition()
        {
            if (MyPictureBox != null)
            {
                MyPictureBox.Left = StartingPosition;
            }
            Location = 0;
        }
    }
}