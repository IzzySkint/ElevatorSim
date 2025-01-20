using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dvt.ElevatorSimulator;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;

namespace ElevatorSimulator.Tests
{
    [TestFixture]
    public class StandardElevatorTests
    {
        [Test]
        public void ValidStandardElevatorInitializeTest()
        {
            StandardElevator elevator = new StandardElevator(10, 10);
            Assert.AreEqual(10, elevator.Number);
            Assert.AreEqual(10, elevator.MaxCapacity);
            Assert.AreEqual(1, elevator.CurrentFloor);
            Assert.AreEqual(0, elevator.CurrentOccupants);
            Assert.AreEqual(ElevatorTypes.Standard, elevator.ElevatorType);
            Assert.AreEqual(ElevatorDirection.Stationary, elevator.Direction);
        }
        [Test]
        public void InvalidStandardElevatorInitializeTest()
        {
            Assert.Throws<ArgumentException>(() => new StandardElevator(0, 10));
            Assert.Throws<ArgumentException>(() => new StandardElevator(-1, 10));
            Assert.Throws<ArgumentException>(() => new StandardElevator(10, 0));
            Assert.Throws<ArgumentException>(() => new StandardElevator(10, -1));
        }

        [Test]
        public void MoveStandardElevatorTest()
        {
           Building building = new Building(10, 1, 10);

           ((StandardElevator)building.Elevators[0]).StandardElevatorArrived += (sender, args) =>
           {
               Assert.AreEqual(5, args.Floor);
               Assert.AreEqual(5, args.NumberOfOccupants);
               if (sender != null)
               {
                   Assert.AreEqual(1, ((StandardElevator)sender).Number);
                   Assert.AreEqual(5, ((StandardElevator)sender).CurrentFloor);
                   Assert.AreEqual(5, ((StandardElevator)sender).CurrentOccupants);

                   ((StandardElevator) sender).RemoveOccupants(args.NumberOfOccupants);
                   Assert.AreEqual(0, ((StandardElevator)sender).CurrentOccupants);
                   building.StopElevators();
               }
           };

           building.DispatchStandardElevator(5, 5);
        }
    }
}
