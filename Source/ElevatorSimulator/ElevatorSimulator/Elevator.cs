namespace Dvt.ElevatorSimulator;

public abstract class Elevator
{
    protected object lockObject;
    public int? DestinationFloor { get; set; }
    private bool IsRunning { get; set; }
    public int MaxCapacity { get; private set; }
    public int Number { get; private set; }
    public ElevatorTypes ElevatorType { get; private set; }
    public int CurrentFloor { get; private set; }
    public ElevatorDirection Direction { get; private set; }

    public abstract bool HasFullCapacity { get; }
    private Thread ElevatorThread { get; set; }
    
    protected Elevator(ElevatorTypes elevatorType, int number, int maxCapacity)
    {
        if (elevatorType == ElevatorTypes.Unknown)
        {
            throw new ArgumentException("Invalid elevator type.");
        }

        if (number < 1)
        {
            throw new ArgumentException("Elevator number must be greater than 0.");
        }

        if (maxCapacity < 1)
        {
            throw new ArgumentException("Elevator max capacity must be greater than 0.");
        }

        lockObject = new object();
        Number = number;
        MaxCapacity = maxCapacity;
        ElevatorType = elevatorType;
        CurrentFloor = 1;
        Direction = ElevatorDirection.Stationary;
        IsRunning = true;
        ElevatorThread = new Thread(Run);
        ElevatorThread.Start();
    }
    
    protected virtual void Run()
    {
        while (IsRunning)
        {
            lock (lockObject)
            {
                if (DestinationFloor.HasValue)
                {
                    MoveToFloor(DestinationFloor.Value);
                    DestinationFloor = null;
                }
            }

            Thread.Sleep(100);
        }
    }

    protected virtual void MoveToFloor(int floor)
    {
        Console.WriteLine($"Elevator {Number} moving from Floor {CurrentFloor} to Floor {floor}...");
        Direction = floor > CurrentFloor ? ElevatorDirection.Up : ElevatorDirection.Down;

        while (CurrentFloor != floor)
        {
            Thread.Sleep(1000);
            CurrentFloor += floor > CurrentFloor ? 1 : -1;
            Console.WriteLine($"Elevator {Number} is now on Floor {CurrentFloor}.");
        }

        Direction = ElevatorDirection.Stationary;
        Console.WriteLine($"Elevator {Number} has arrived at Floor {CurrentFloor}.");
    }

    public int GetNumberOfFloorsToDestination(int floor)
    {
        return Math.Abs(CurrentFloor - floor);
    }

    public abstract void DisplayStatus();

    public void Stop()
    {
        IsRunning = false;
        ElevatorThread.Join();
        Console.WriteLine($"Elevator {Number} has been stopped.");
    }
}