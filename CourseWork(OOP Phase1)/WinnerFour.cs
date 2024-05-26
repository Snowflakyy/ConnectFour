using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_OOP_Phase1_
{
    public sealed class WinnerFour

    {
        public bool PlayerWon { get; private set; }
        public HashSet<Point> WinningLocation { get; private set; }
        public WinnerFour(bool playerWon, HashSet<Point> winningLocation)
        {
            PlayerWon = playerWon;
            WinningLocation = winningLocation;
        }
    }
}
