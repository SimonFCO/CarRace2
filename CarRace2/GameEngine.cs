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
        public static bool raceOn = true;
        private const int WinningDistance = 5000;
        public static int TickSpeed = 100;
        public static void RunGame()
        {
            InitCars();
            Console.WriteLine("Press any key to begin the race");
            Console.ReadKey();
            while (raceOn)
            {
                Console.Clear();
                GameMenu();
                Thread.Sleep(TickSpeed);
            }
        }

        public static void StopTheRace()
        {
            raceOn = false;
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
                Console.Write($"Car: {car.Name}, Distance Driven: {car.DistanceDriven}m, Speed: {car.Speed} Km/h, Broken: {car.Stopped} ");
                if(car.HasWon == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" WE HAVE A WINNER!!!");
                    StopTheRace();
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
        static void GameTick(Car car)
        {
            bool HasOtherCarWon = false;

            while(!car.HasWon && !HasOtherCarWon)
            {
                car.Seconds += 1;
                foreach (Car otherCar in carList)
                {
                    if (otherCar.HasWon)
                    {
                        HasOtherCarWon = true;
                    }

                }
                if (!car.Stopped)
                {
                    if (car.Seconds % 10 == 0)
                    {
                        Events.ExecuteRandomEvent(car);
                    }
                }
                car.Drive();
                if (car.DistanceDriven >= WinningDistance)
                {
                    car.HasWon = true;
                }
                Thread.Sleep(TickSpeed);
            }
        }
    }
}
