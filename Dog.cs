using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsAtTheRaces
{
    internal class Dog
    {
        public PictureBox MyPictureBox;
        public int Location;
        public int StartingPosition;
        public int RaceTrackLength;

        public static Random Randomizer = new Random();

        public bool Run()
        {
            Location += Randomizer.Next(1, 5);

            MyPictureBox.Left = StartingPosition + Location;

            return (MyPictureBox.Left >= RaceTrackLength);
        }

        public void TakeStartingPosition()
        {
            MyPictureBox.Left = StartingPosition;
            Location = 0;
        }
    }
}