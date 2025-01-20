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

        var elevators = new List<Elevator>(NumberOfElevators);

        for (var counter = 1; counter <= NumberOfElevators; counter++)
        {
            elevators.Add(new StandardElevator(counter, elevatorMaxCapacity));
        }

        Elevators = elevators;

        foreach (var elevator in Elevators)
        {
            if (elevator is StandardElevator standardElevator)
            {
                standardElevator.StandardElevatorArrived += OnStandardElevatorArrived;
            }
        }
    }

    public int NumberOfFloors { get; private set; }
    public int NumberOfElevators { get; private set; }
    public IReadOnlyList<Elevator> Elevators { get; private set; }

    public void StopElevators()
    {
        foreach (var elevator in Elevators)
        {
            elevator.Stop();
        }
    }

    public void DispatchStandardElevator(int floor, int numberOfOccupants)
    {
        if(floor < 1 || floor > NumberOfFloors)
        {
            throw new InvalidFloorException($"Invalid floor number. Please enter a number between 1 and {NumberOfFloors}.");
            return;
        }

        var elevators = Elevators.Where(e => !e.HasFullCapacity);
        var elevatorList = elevators.ToList();

        if (elevatorList.Count == 0)
        {
            Console.WriteLine("All elevators are at full capacity. Please wait for an elevator to become available.");
            return;
        }

        int numberOfOccupantsCount = numberOfOccupants;

        while(numberOfOccupantsCount > 0)
        {
            foreach (var elevator in elevatorList)
            {
                if(elevator.ElevatorType == ElevatorTypes.Standard)
                {
                    var standardElevator = (StandardElevator)elevator;
                    
                    var occupantsToAdd = standardElevator.CurrentOccupants + numberOfOccupantsCount <= standardElevator.MaxCapacity
                        ? standardElevator.MaxCapacity - standardElevator.CurrentOccupants
                        : numberOfOccupantsCount - standardElevator.MaxCapacity;

                    if (occupantsToAdd > 0)
                    {
                        standardElevator.AddOccupants(occupantsToAdd);
                        numberOfOccupantsCount -= occupantsToAdd;
                        standardElevator.DestinationFloor = floor;
                    }

                    if(numberOfOccupantsCount == 0)
                    {
                        break;
                    }
                }
            }
        }
    }

    private void OnStandardElevatorArrived(object? sender, ElevatorEventArgs e)
    {
        if(sender == null)
        {
            return;
        }

        var standardElevator = (StandardElevator)sender;
        standardElevator.RemoveOccupants(e.NumberOfOccupants);
        standardElevator.StandardElevatorArrived -= OnStandardElevatorArrived;
    }
}