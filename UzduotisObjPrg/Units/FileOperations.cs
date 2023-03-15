using System.Text.RegularExpressions;
using UzduotisObjPrg.Objects;

namespace UzduotisObjPrg.Units;

public class FileOperation
{
    public static List<String> WordList { get; set; } = new List<string>();

    private string _wordListPath = @"C:\Users\DOM\RiderProjects\UzduotisObjPrg\UzduotisObjPrg\Wordlist.txt";
    private string _usersTxtPath = @"C:\Users\DOM\RiderProjects\UzduotisObjPrg\UzduotisObjPrg\scoreboard.txt";
    
    public FileOperation()
    {
        if (WordList.Count == 0) GetWordList();
    }

    private void GetWordList()
    {
        WordList = new List<string>(ReadValuesFromFile(_wordListPath));
    }
    
    private List<string> ReadValuesFromFile(string path)
    {
        var list = new List<string>();
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string? value = reader.ReadLine();
                if (String.IsNullOrEmpty(value)) continue;
                list.Add(value);
            }
        }
        return list;
    }

    public Player GetPlayer(string playerName)
    {
        var playerStrList = ReadValuesFromFile(_usersTxtPath);
        var playerList = playerStrList.ConvertAll<Player>(
            pl => new Player(pl.Split(" ")[0], int.Parse(pl.Split("0")[1])));
        
        return playerList.SingleOrDefault(
            p => p.Name.Equals(playerName, StringComparison.CurrentCultureIgnoreCase),
            new Player(playerName));
    }
    
    
    
}