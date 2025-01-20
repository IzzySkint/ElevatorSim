using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvt.ElevatorSimulator
{
    public class FullCapacityException : Exception
    {
        public FullCapacityException() : base()
        {
        }

        public FullCapacityException(string message) : base(message)
        {
        }

        public FullCapacityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
