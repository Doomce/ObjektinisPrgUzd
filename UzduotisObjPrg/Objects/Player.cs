namespace UzduotisObjPrg.Objects;

public class Player
{
    public string Name { get; }
    public int Points { get; set; } = 0;

    public Player(string name)
    {
        Name = name;
    }
    
    public Player(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public void PrintStats()
    {
        Console.WriteLine("------ Tavo statistika ------");
        Console.WriteLine($"   Turi taškų: {this.Points}");
        Console.WriteLine($"------ {this.Name} ------");
    }
}