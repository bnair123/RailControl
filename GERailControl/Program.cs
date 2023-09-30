using System;
using System.Threading.Tasks;

namespace TrainControl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Train Control Software!");

            var api = new API();
            var trainController = new TrainController(api);
            var console = new Console(trainController);

            while (true)
            {
                console.PrintMenu();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await console.PrintTrains();
                        break;
                    case "2":
                        await console.PrintStations();
                        break;
                    case "3":
                        await console.PrintAverageValues();
                        break;
                    case "4":
                        await console.StopTrains();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}