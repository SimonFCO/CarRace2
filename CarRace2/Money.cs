using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace2
{
    internal class Money
    {
        public static int Balance;
        public static string CarBet;
        public static int BetSize;

        public static void InitGambling()
        {
            Balance = 100;
            CarBet = "";
        }
        public static void GamblingStartUi()
        {
            Console.Clear();
            bool carPicked = false;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Hello Sir you have: "+Balance);
            Console.WriteLine("What car do you want to bet on?");
            Console.WriteLine("1, Toyota");
            Console.WriteLine("2, Volvo");
            Console.WriteLine("3, Saab");

            while (!carPicked)
            {
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.WriteLine("You pick toyota, and how much do you want to bet?");
                    CarBet = "Toyota";
                    carPicked = true;
                }
                else if (answer == "2")
                {
                    Console.WriteLine("You pick Volvo, and how much do you want to bet?");
                    CarBet = "Volvo";
                    carPicked = true;
                }
                else if (answer == "3")
                {
                    Console.WriteLine("You pick Saab, and how much do you want to bet?");
                    CarBet = "Saab";
                    carPicked = true;
                }
                else
                {
                    Console.WriteLine("Write a real number");
                }

            }

            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < 0)
            {
                Console.WriteLine("You must enter a valid number!");
            }

            BetSize = input;
            Console.WriteLine("Alright you choose to bet " + BetSize);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void GamblingEnd()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (Ui.Winners[0].Contains(CarBet, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Congrats you won " + BetSize * 2);
                Balance += BetSize * 2;
                Console.WriteLine("You now have: " + Balance);
                BetSize = 0;
                CarBet = "";
                Console.WriteLine("Do you want to play again?");
                Console.WriteLine("1, Continue");
                Console.WriteLine("2, End");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("ooooh sorry you lost, better luck next time!");
                Balance -= BetSize;

                Console.WriteLine("Do you want to play again?");
                Console.WriteLine("1, Continue");
                Console.WriteLine("2, End");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.Clear();
                }
                else
                {
                    GameEngine.KeepPlaying = false;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
   
}
