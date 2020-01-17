using Autofac;
using Kneat.SW.Application.Command.Starships;
using Kneat.SW.Ioc;
using MediatR;
using System;
using System.Text;

namespace Kneat.SW.ConsoleApp
{
    class Program
    {
        private static IContainer _container { get; set; }
        private static IMediator _mediator { get => _container?.Resolve<IMediator>(); }

        static void Main(string[] args)
        {
            var menuChosen = 0;

            Console.OutputEncoding = Encoding.Default;
            Console.WriteLine("Starting Application...");

            LoadIocContainer();

            do
            {
                Console.Clear();

                Console.Write("..:: Star Wars - Api Client ::..");
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}1 - Show all starships");
                Console.WriteLine("2 - Find starship by id");
                Console.WriteLine("3 - Show stops needed to resupply (all starships)");
                Console.WriteLine("4 - Show stops needed to resupply (specific starship)");

                Console.Write($"{Environment.NewLine}Type your choice: ");

                while (!int.TryParse(Console.ReadLine(), out menuChosen))
                {
                    Console.Write("Invalid input. Try again: ");
                }

                switch (menuChosen)
                {
                    case 1:
                        ShowAllStarships();
                    break;
                    case 2:
                        ShowStarshipById();
                    break;
                    case 3:
                        ShowAllStopsNeededToResupply();
                    break;
                    case 4:
                        ShowStopsNeededToResupplyById();
                    break;
                    default:
                        Console.WriteLine($"{Environment.NewLine}Invalid value chosen.");
                    break;
                }

                Console.Write($"{Environment.NewLine}Press ESC to exit or any else key to continue...");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            _container.Dispose();
        }

        private static void ShowAllStarships()
        {
            Console.Clear();
            Console.Write("Please wait. Loading Starships...");

            var command = new FindAllStarshipsCommand();
            var starships = _mediator.Send(command).Result;

            Console.Clear();
            Console.WriteLine($"..:: Starships ::..{Environment.NewLine}");

            foreach (var starship in starships)
            {
                Console.WriteLine(starship.ToString());
            }
        }

        private static void ShowStarshipById()
        {
            var id = 0;

            Console.Clear();
            Console.Write("Type starship id: ");

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Try again: ");
            }

            Console.Write("Please wait. Loading Starship...");

            var command = new FindStarshipByIdCommand(id);
            var starship = _mediator.Send(command).Result;

            Console.Clear();

            if (starship != null)
            {
                Console.WriteLine($"..:: Starship ::..{Environment.NewLine}");
                Console.WriteLine(starship.ToString());
            }
            else
            {
                Console.Write($"Cannot find the starship with this id.{Environment.NewLine}");
            }
        }

        private static void ShowAllStopsNeededToResupply()
        {
            long distance = 0;

            Console.Clear();
            Console.Write("Type distance: ");

            while (!long.TryParse(Console.ReadLine(), out distance))
            {
                Console.Write("Invalid input. Try again: ");
            }
            Console.Write("Please wait. Loading Starships...");

            var command = new FindAllStarshipsCommand();
            var starships = _mediator.Send(command).Result;

            Console.Clear();
            Console.WriteLine($"..:: Starships ::..{Environment.NewLine}");

            foreach (var starship in starships)
            {
                var stops = starship.GetStopsNeededToResupply(distance);
                var stopsDescription = stops > 0 ? stops.ToString() : "Cannot be calculated.";
                Console.WriteLine($"Name: {starship.Name}");
                Console.WriteLine($"Stops Needed: {stopsDescription}{Environment.NewLine}");
            }
        }

        private static void ShowStopsNeededToResupplyById()
        {
            long distance = 0;
            var id = 0;

            Console.Clear();
            Console.Write("Type starship id: ");

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Try again: ");
            }

            Console.Write("Type distance: ");

            while (!long.TryParse(Console.ReadLine(), out distance))
            {
                Console.Write("Invalid input. Try again: ");
            }

            Console.Write("Please wait. Loading Starship...");

            var command = new FindStarshipByIdCommand(id);
            var starship = _mediator.Send(command).Result;

            Console.Clear();

            if (starship != null)
            {
                var stops = starship.GetStopsNeededToResupply(distance);
                var stopsDescription = stops > 0 ? stops.ToString() : "Cannot be calculated.";

                Console.WriteLine($"..:: Starship ::..{Environment.NewLine}");
                Console.WriteLine($"Name: {starship.Name}");
                Console.WriteLine($"Stops Needed: {stopsDescription}{Environment.NewLine}");
            }
            else
            {
                Console.Write($"Cannot find the starship with this id.{Environment.NewLine}");
            }
        }

        private static void LoadIocContainer()
        {
            Console.WriteLine("Loading autofac context...");
            _container = new ApplicationContextBuilder().Build();
        }
    }
}
