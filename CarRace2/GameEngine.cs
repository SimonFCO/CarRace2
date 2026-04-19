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
        private const int WinningDistance = 1000;
        public static int TickSpeed = 500;

        public static bool KeepPlaying = true;
        public static void RunGame()
        {
            while (KeepPlaying)
            {
                InitCars();

                // FOR FUN

                Money.InitGambling();
                Money.GamblingStartUi();

                // END FOR FUN

                Console.WriteLine("Press any key to begin the race");
                Console.ReadKey();
                while (raceOn)
                {
                    CheckWinners();
                    Console.Clear();
                    Ui.TopPartMenu(carList);
                    Ui.MidPartMenu();
                    Thread.Sleep(TickSpeed);

                }
                Ui.BotPartMenu();

                // FOR FUN (again..)

                Money.GamblingEnd();

                // END FOR FUN (again..)
            }

        }

        public static void CheckWinners()
        {
            int CarsThatHaveWon = 0;
            foreach(Car car in carList)
            {
                if(car.HasWon)
                {
                    CarsThatHaveWon += 1;
                }
                
            }
            if (CarsThatHaveWon == 3)
            {
                StopTheRace();
            }
        }
        public static void StopTheRace()
        {
            raceOn = false;
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

            while(raceOn && !car.HasWon)
            {
                car.Seconds += 1;
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
                    Ui.AddWinner($"Time:{car.Seconds} | Name:{car.Name}");
                }
                Thread.Sleep(TickSpeed);
            }
        }
    }
}
