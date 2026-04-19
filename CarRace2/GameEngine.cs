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
            while (true)
            {
                Console.Clear();
                GameMenu();
                Thread.Sleep(1000);
            }
        }

        public static void GameMenu()
        {
            Console.WriteLine("Game Menu - Car Status:");
            foreach (var car in carList)
            {
                Console.WriteLine($"Car: {car.Name}, Distance Driven: {car.DistanceDriven}, Speed: {car.Speed}, Broken: {car.Stopped}");
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
            while(car.DistanceDriven <= 1000)
            {
                car.Drive();
                Thread.Sleep(1000);
            }
            return true;
        }
    }
}
