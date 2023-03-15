using System.Collections;
using System.Text.RegularExpressions;
using UzduotisObjPrg.Objects;
using UzduotisObjPrg.Units;

namespace UzduotisObjPrg;

public class Game
{
    private Player _player;
    private GameLevel _level;
    private int _tries;

    public Game(GameLevel lvl, string plName)
    {
        //new FileOperation inicializuoja ir wordlista.
        _player = new FileOperation().GetPlayer(plName);
        _level = lvl;
        
        Console.WriteLine("");
        WordGame();
    }

    private void WordGame()
    {
        _tries = (int) _level;

        try
        {
            string word = Randomizer.GetRandomWord();
            List<char> guessedChars = new List<char>();
            
            while (true)
            {
                if (_tries == 0)
                {
                    Console.WriteLine("Žodis turėjo būti: "+word);
                    break;
                };
                Console.WriteLine(GetGuessString(word, guessedChars));
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                if (CheckInput(word, input, ref guessedChars)) break;
            }
        } catch (RandomizerException) {
            Console.WriteLine("Žaidimas baigėsi, nes nebeliko žodžių.....");
            return;
        }
        
        _player.PrintStats();

        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.T) WordGame();
            if (Console.ReadKey().Key == ConsoleKey.N) break;
        }
    }
    
    
    private string GetGuessString(string word, List<char> guessedChars)
    {
        if (guessedChars.Count > 0)
        {
            return Regex.Replace(word, $"[^"+String.Join(",", guessedChars)+"]", "_");;
        }
        return Regex.Replace(word, @"\w", "_");
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns>TRUE, jei nebereikia sukti ciklo. False - jei ji dar reikia sukti</returns>
    private bool CheckInput(string word, string input, ref List<char> guessedChars)
    {
        if (_tries == 0) return true;
        
        if (input.Length > 1)
        {
            if (word.Equals(input, StringComparison.CurrentCultureIgnoreCase))
            {
                _player.Points += 100;
                _player.Points += 50 * _tries;
                _tries = (int) _level;
                Console.WriteLine("Atspėjai žodį.");
                return true;
            }
            _tries--;
            Console.WriteLine($"Neteisingas bandymas. Liko {_tries} bandymai.");
            return false;
        }

        if (guessedChars.Contains(input.ToLower()[0]))
        {
            Console.WriteLine("Ši raidė jau panaudota!");
            return false;
        }
        
        if (word.Contains(input[0]))
        {
            guessedChars.Add(input[0]);
        }
        else
        {
            _tries--;
            Console.WriteLine($"Neteisingas bandymas. Liko {_tries} bandymai.");
        }

        if (word.Equals(GetGuessString(word, guessedChars)))
        {
            _player.Points += 100;
            _player.Points += 50 * _tries;

            Console.WriteLine("Atspėjai žodį paraidžiui.");
            return true;
        }
        
        return false;
    }
    
    
}