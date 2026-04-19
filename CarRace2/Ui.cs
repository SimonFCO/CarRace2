using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace2
{
    internal class Ui
    {
        public static Queue<string> Events = new Queue<string>();
        public static List<string> Winners = new List<string>();
        private static readonly object _eventLock = new object();
        public static void TopPartMenu(List<Car> carList)

        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Menu - Car Status:");
            foreach (var car in carList) // Top part of the menu
            {
                if (car.Stopped)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write($"Car: {car.Name}, Distance Driven: {car.DistanceDriven}m, Speed: {car.Speed} Km/h, Broken: {car.Stopped} ");
                if (car.HasWon == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" WE HAVE A WINNER!!!");
                }
                else if (car.Stopped)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(car.Problem);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("--------------------------------------------------------------------\n\n");

        }
        public static void AddEvent(string eventString)
        {
            lock (_eventLock)
            Events.Enqueue(eventString);
            if(Events.Count > 12)
            {
                Events.Dequeue();
            }
        }
        public static void AddWinner(string WinnersString)
        {
            lock (_eventLock)
            Winners.Add(WinnersString);
        }
        public static void MidPartMenu()
        {
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            lock (_eventLock)
            Console.WriteLine("Event Log:");
            if (Events.Count == 0)
            {
                Console.WriteLine("No events to display.");
            }
            else
            {
                foreach (string Event in Events)
                {
                    Console.WriteLine(Event);
                }
            }
            Console.WriteLine("--------------------------------------------------------------------\n\n");
        }

        public static void BotPartMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            lock (_eventLock)

            Console.WriteLine("Event Log:");
            Console.WriteLine($"First Place  | {Winners[0]}");
            Console.WriteLine($"Second Place | {Winners[1]}");
            Console.WriteLine($"Third Place  | {Winners[2]}");
        }
    }
}
