// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using UzduotisObjPrg;
using UzduotisObjPrg.Objects;
using UzduotisObjPrg.Units;

internal class Program
{
    public static void Main(string[] args)
    {
        FileOperation.GetWordList();
        if (FileOperation.WordList.Count == 0)
        {
            Console.WriteLine("Wordlist tuščias.");
            return;
        }

        Console.WriteLine("Iš programos galite išeiti naudodami mygtuką - ESC");
        Console.WriteLine("Gerą diena, įveskite savo vardą:");
        var name = Console.ReadLine();
        if (name == null)
        {
            Console.WriteLine("BLOGAS VARDAS");
            Process.GetCurrentProcess().Kill();
            return;
        }
        Console.WriteLine("Pasirinkite žaidimo sunkumą:");
        Console.WriteLine("[1] - Lengvas.");
        Console.WriteLine("[2] - Vidutinis.");
        Console.WriteLine("[3] - Sunkus.");
        
        while (true)
        {
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                {
                    new Game(GameLevel.Easy, name);
                    break;
                }
                case ConsoleKey.D2:
                {
                    new Game(GameLevel.Normal, name);
                    break;
                }
                case ConsoleKey.D3:
                {
                    new Game(GameLevel.Hard, name);
                    break;
                }
            }

            break;


        }


    }
}
