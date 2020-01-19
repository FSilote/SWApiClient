using Autofac;
using Kneat.SW.Application.Command.Starships;
using Kneat.SW.Domain.Exceptions;
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

            StartApplication();

            do
            {
                try
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
                }
                catch (BaseException baseEx)
                {
                    // We could log and do other actions to business and infrastructure errors...
                    Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Sorry, something looks like wrong: {baseEx.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Sorry, something looks like wrong: {e.InnerException?.Message ?? e.Message}");
                }
                finally
                {
                    Console.Write($"{Environment.NewLine}Press ESC to exit or any else key to continue...");
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            _container.Dispose();
        }

        private static void ShowAllStarships()
        {
            Console.Clear();
            Console.Write("Please wait. Loading Starships...");

            var command = new FindAllStarshipsCommand();
            // Need use ".Result" instend of "async await" to lock console application
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
                Console.WriteLine($"Stops Required: {stopsDescription}{Environment.NewLine}");
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
                Console.WriteLine($"Stops Required: {stopsDescription}{Environment.NewLine}");
            }
            else
            {
                Console.Write($"Cannot find the starship with this id.{Environment.NewLine}");
            }
        }

        private static void StartApplication()
        {
            try
            {
                Console.OutputEncoding = Encoding.Default;
                Console.WriteLine("Starting Application...");
                Console.WriteLine("Loading autofac context...");
                _container = new ApplicationContextBuilder().Build();
            }
            catch
            {
                Console.Write($"{Environment.NewLine}Could not start application. Please, retry later. Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
