using System;

namespace KolkoKrzyzyk
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Witaj w grze!");
            System.Threading.Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.White;
            Game abc = new Game();
            abc.ShowMenu();
            abc.GetChoice();
            Console.Clear();
        }


    }
}
