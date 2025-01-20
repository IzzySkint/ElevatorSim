using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvt.ElevatorSimulator
{
    public class InvalidFloorException : Exception
    {
        public InvalidFloorException() : base()
        {
        }
        public InvalidFloorException(string message) : base(message)
        {
        }
        public InvalidFloorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
