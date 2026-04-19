using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace2
{
    internal class Events
    {
        
        private static void EmptyGas(Car car)
        {
            car.Stopped = true;
            car.PauseTimer += 15;
        }

        private static void BlownTire(Car car)
        {
            car.Stopped = true;
            car.PauseTimer += 10;
        }

        private static void BirdHit(Car car)
        {
            car.Stopped = true;
            car.PauseTimer += 5;
        }

        private static void EngineProblem(Car car)
        {
            car.Speed -= 1;
        }
        public static void ExecuteRandomEvent(Car car)
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 51);

            switch (random)
            {
                case int n when (n >= 1 && n <= 2):
                    car.Problem = "Slut på bensin";
                    EmptyGas(car);
                    break;

                case int n when (n >= 3 && n <= 4): 
                    car.Problem = "Punktering";
                    BlownTire(car);
                    break;

                case int n when (n >= 5 && n <= 9): 
                    car.Problem = "Fågel på vindrutan";
                    BirdHit(car);
                    break;

                case int n when (n >= 10 && n <= 19): 
                    car.Problem = "Motorfel";
                    EngineProblem(car);
                    break;
                default:
                    car.Problem = "";
                    break;
            }
        }
    }
}
