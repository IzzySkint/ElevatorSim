using Dvt.ElevatorSimulator;

namespace ElevatorSimulator.Tests
{
    [TestFixture]
    public class BuildingTests
    {
        [Test]
        public void ValidBuildingInitializeTest()
        {
            Building building = new Building(10, 2, 10);

            Assert.AreEqual(10, building.NumberOfFloors);
            Assert.AreEqual(2, building.NumberOfElevators);
            Assert.AreEqual(2, building.Elevators.Count);
        }

        [Test]
        public void InvalidBuildingInitializeTest()
        {
            Assert.Throws<ArgumentException>(() => new Building(0, 2, 10));
            Assert.Throws<ArgumentException>(() => new Building(-1, 2, 10));
            Assert.Throws<ArgumentException>(() => new Building(10, 0, 10));
            Assert.Throws<ArgumentException>(() => new Building(10, -1, 10));
            Assert.Throws<ArgumentException>(() => new Building(10, 2, 0));
            Assert.Throws<ArgumentException>(() => new Building(10, 2, -1));
        }

        [Test]
        public void DispatchStandardElevatorTest()
        {
            Building building = new Building(10, 2, 10);
            building.DispatchStandardElevator(1, 5);
            building.DispatchStandardElevator(10, 5);
            Assert.Throws<InvalidFloorException>(() => building.DispatchStandardElevator(0, 5));
            Assert.Throws<InvalidFloorException>(() => building.DispatchStandardElevator(11, 5));
            building.StopElevators();
        }
    }
}