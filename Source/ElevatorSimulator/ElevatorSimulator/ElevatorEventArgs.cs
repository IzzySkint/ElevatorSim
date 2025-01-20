using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvt.ElevatorSimulator
{
    public class ElevatorEventArgs(int floor, int numberOfOccupants) : EventArgs
    {
        public int Floor { get; private set; } = floor;
        public int NumberOfOccupants { get; private set; } = numberOfOccupants;
    }
}
