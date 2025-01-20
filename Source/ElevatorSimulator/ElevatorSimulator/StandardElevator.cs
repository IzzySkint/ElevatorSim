namespace Dvt.ElevatorSimulator;

public class StandardElevator(int number, int numberOfFloors, int maxCapacity)
    : Elevator(ElevatorTypes.Standard, number, numberOfFloors)
{
    private int MaxCapacity { get; set; } = maxCapacity;
    private int CurrentOccupants { get; set; }
    public override bool IsOverloaded => CurrentOccupants > MaxCapacity;

    public void AddOccupants(int occupants)
    {
        lock (lockObject)
        {
            if (CurrentOccupants + occupants > MaxCapacity)
            {
                Console.WriteLine($"Elevator {Number} cannot accommodate {occupants} passengers. Max capacity: {MaxCapacity}.");
            }
            else
            {
                CurrentOccupants += occupants;
                Console.WriteLine($"{occupants} passengers entered Elevator {Number}.");
            }
        }
    }

    public void RemoveOccupants(int passengers)
    {
        lock (lockObject)
        {
            CurrentOccupants = Math.Max(0, CurrentOccupants - passengers);
            Console.WriteLine($"{passengers} passengers exited Elevator {Number}.");
        }
    }
    
    public override void DisplayStatus()
    {
        lock (lockObject)
        {
            Console.WriteLine($"Elevator {Number} | Floor: {CurrentFloor} | Occupants: {CurrentOccupants}/{MaxCapacity} | Direction: {Direction}");
        }
    }
}