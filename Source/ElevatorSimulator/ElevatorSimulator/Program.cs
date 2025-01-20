// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

namespace Dvt.ElevatorSimulator;

public class Program
{
    private static Building building;

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Elevator Simulator!");
        Console.WriteLine("Lets get started setting up the building...");

        while (true)
        {
            try
            {
                Console.WriteLine("Please enter the number of floors in the building:");
                var numberOfFloors = int.Parse(Console.ReadLine() ?? "10");
                Console.WriteLine("Please enter the number of elevators in the building:");
                var numberOfElevators = int.Parse(Console.ReadLine() ?? "2");
                Console.WriteLine("Please enter the maximum capacity of each elevator:");
                var elevatorMaxCapacity = int.Parse(Console.ReadLine() ?? "10");
                building = new Building(numberOfFloors, numberOfElevators, elevatorMaxCapacity);
                Console.WriteLine("Building setup complete!");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again.");
                Console.ReadKey(true);
                return;
            }
        }

        StartSimulation();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
    }

    private static void StartSimulation()
    {
        while(true)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Call Elevator");
            Console.WriteLine("2. Exit");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Please enter the floor you want to go to:");
                    int.TryParse(Console.ReadLine(), out int floor);
                    Console.WriteLine("Please enter the number of occupants:");
                    int.TryParse(Console.ReadLine(), out int occupants);
                    if (floor > building.NumberOfFloors || floor < 1)
                    {
                        Console.WriteLine($"Invalid floor number. Please enter a number between 1 and {building.NumberOfFloors}.");
                        break;
                    }
                    building.DispatchStandardElevator(floor, occupants);
                    break;
                case "2":
                    Console.WriteLine("Exiting Elevator Simulator...");
                    building.StopElevators();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}