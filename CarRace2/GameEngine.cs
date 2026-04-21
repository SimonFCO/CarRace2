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
        public static int TickSpeed = 20;

        public static bool KeepPlaying = true;
        public static void RunGame()
        {
            // FOR FUN
            Money.InitGambling();
            // END FOR FUN

            while (KeepPlaying)
            {
                raceOn = true;
                // FOR FUN
                Money.GamblingStartUi();
                // END FOR FUN           

                Console.WriteLine("Press any key to begin the race");                
                Console.ReadKey();
                Console.Clear();
                InitCars();
                Update();
                while (raceOn)
                {
                    Console.WriteLine("Write Status or press any key for a update");
                    string answer = "";
                    answer = Console.ReadLine();
                    if (answer == "Status" || answer == "status")
                    {
                        Update();
                    }
                    else if(string.IsNullOrEmpty(answer))
                    {
                        Update();
                    }
                }

                // FOR FUN (again..)
                Money.GamblingEnd();
                Ui.Winners.Clear();
                Ui.Events.Clear();
                carList.Clear();
                // END FOR FUN (again..)
            }

        }

        public static void Update()
        {
            Console.Clear();
            Ui.TopPartMenu(carList);
            Ui.MidPartMenu();
            Thread.Sleep(TickSpeed);
            if (!raceOn)
            {
                Ui.BotPartMenu();
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
                Console.Clear();
            }
            if (!raceOn)
            {
                Console.Clear();
                Ui.BotPartMenu();
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

            Console.WriteLine("The race has officially started!");
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

                    if (Ui.Winners.Count == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"TIME:{DateTime.Now.ToString("hh\\:mm\\:ss")}: {car.Name} HAS CROSSED THE FINISH LINE AND WON THE RACE!");
                        Ui.AddEvent($"TIME:{DateTime.Now.ToString("hh\\:mm\\:ss")}: {car.Name} HAS CROSSED THE FINISH LINE AND WON THE RACE!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"TIME:{DateTime.Now.ToString("hh\\:mm\\:ss")}: {car.Name} has crossed the finish line!");
                        Ui.AddEvent($"TIME:{DateTime.Now.ToString("hh\\:mm\\:ss")}: {car.Name} has crossed the finish line!");
                    }
                }
                Thread.Sleep(TickSpeed);
            }
            CheckWinners();
        }
    }
}
