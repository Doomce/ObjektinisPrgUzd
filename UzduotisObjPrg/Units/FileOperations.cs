using System.Security;
using System.Text.RegularExpressions;
using UzduotisObjPrg.Objects;

namespace UzduotisObjPrg.Units;

public static class FileOperation
{
    public static List<String> WordList { get; set; } = new List<string>();

    private static string _wordListPath = @"C:\Users\DOM\RiderProjects\UzduotisObjPrg\UzduotisObjPrg\Wordlist.txt";
    private static string _usersTxtPath = @"C:\Users\DOM\RiderProjects\UzduotisObjPrg\UzduotisObjPrg\scoreboard.txt";

    public static void GetWordList()
    {
        WordList = new List<string>(File.ReadAllLines(_wordListPath));
    }
    
    private static List<Player> ReadValuesFromFile(string path)
    {
        try
        {
            return File.ReadAllLines(_usersTxtPath).ToList().ConvertAll<Player>(
                pl => new Player(pl.Split(" ")[0], int.Parse(pl.Split(" ")[1])));
        }
        catch (Exception exception) when (exception is FileNotFoundException or SecurityException)
        {
            return new List<Player>();
        }
    }

    public static void SavePlayer(Player pl)
    {
        var list = ReadValuesFromFile(_usersTxtPath);
        var index = list.FindIndex(player => player.Name.Equals(pl.Name));
        
        if (index >= 0) list[index].Points = pl.Points;
        else list.Add(pl);
        
        var newList = list.ConvertAll<string>(player => $"{player.Name} {player.Points}");
        File.WriteAllLines(_usersTxtPath, newList);
    }

    public static Player GetPlayer(string playerName)
    {
        return ReadValuesFromFile(_usersTxtPath).SingleOrDefault(
            p => p.Name.Equals(playerName, StringComparison.CurrentCultureIgnoreCase),
            new Player(playerName));
    }
    
    
    
}