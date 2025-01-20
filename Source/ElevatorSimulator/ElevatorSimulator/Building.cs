namespace Dvt.ElevatorSimulator;

public class Building
{
    public Building(int numberOfFloors, int numberOfElevators, int elevatorMaxCapacity)
    {
        if (numberOfFloors < 2)
        {
            throw new ArgumentException("Number of floors must be greater than 1");
        }

        if (numberOfElevators <= 0)
        {
            throw new ArgumentException("Number of elevators must be greater than 0");
        }

        if (elevatorMaxCapacity < 1)
        {
            throw new ArgumentException("Elevator max capacity must be greater than or equal to 1");
        }

        NumberOfFloors = numberOfFloors;
        NumberOfElevators = numberOfElevators;

        Elevators = new List<Elevator>(NumberOfElevators);

        for (var counter = 1; counter <= NumberOfElevators; counter++)
        {
            Elevators.Add(new StandardElevator(counter, NumberOfFloors, 10));
        }
    }

    private int NumberOfFloors { get; set; }
    private int NumberOfElevators { get; set; }
    private List<Elevator> Elevators { get; set; }
}