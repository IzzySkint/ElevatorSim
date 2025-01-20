namespace Dvt.ElevatorSimulator;

public class StandardElevator(int number, int maxCapacity)
    : Elevator(ElevatorTypes.Standard, number, maxCapacity)
{
    public event EventHandler<ElevatorEventArgs>? StandardElevatorArrived;

    public int CurrentOccupants { get; private set; }
    public override bool HasFullCapacity => CurrentOccupants >= MaxCapacity;

    public void AddOccupants(int occupants)
    {
        lock (lockObject)
        {
            if (CurrentOccupants + occupants > MaxCapacity)
            {
                Console.WriteLine($"Elevator {Number} cannot accommodate {occupants} passengers. Max capacity: {MaxCapacity}.");
                throw new FullCapacityException($"Elevator {Number} cannot accommodate {occupants} passengers. Max capacity: {MaxCapacity}.");
            }
            else
            {
                CurrentOccupants += occupants;
                Console.WriteLine($"{occupants} passengers entered Elevator {Number}.");
            }
        }
    }

    protected override void MoveToFloor(int floor)
    {
        base.MoveToFloor(floor);

        StandardElevatorArrived?.Invoke(this, new ElevatorEventArgs(floor, CurrentOccupants));
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