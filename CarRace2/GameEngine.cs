using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace2
{
    internal class GameEngine
    {
        public static List<Thread> threads = new List<Thread>();
        public static List<Car> carList = new List<Car>();
        public static void RunGame()
        {
            InitCars();
            Console.WriteLine("Press any key to begin the race");
            Console.ReadKey();
            while (true)
            {
                Console.Clear();
                GameMenu();
                Thread.Sleep(1000);
            }
        }

        public static void GameMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Menu - Car Status:");
            foreach (var car in carList)
            {
                if (car.Stopped)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                }
                Console.Write($"Car: {car.Name}, Distance Driven: {car.DistanceDriven}, Speed: {car.Speed}, Broken: {car.Stopped} ");
                if(car.HasWon == true)
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
        }

        public static void InitCars()
        {
            carList.Add(new Car("Toyota"));
            carList.Add(new Car("Volvo"));
            carList.Add(new Car("Saab"));

            foreach (Car car in carList)
            {
                Thread thread = new Thread(() => GameTick(car));
                threads.Add(thread);
                thread.Start();
            }
        }
        static bool GameTick(Car car)
        {
            while(!car.HasWon)
            {
                if (!car.Stopped)
                {
                    Events.ExecuteRandomEvent(car);
                }
                car.Drive();
                if (car.DistanceDriven >= 1000)
                {
                    car.HasWon = true;
                }
                Thread.Sleep(1000);
            }
            return true;
        }
    }
}
