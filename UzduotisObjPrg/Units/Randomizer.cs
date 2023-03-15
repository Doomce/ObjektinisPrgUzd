namespace UzduotisObjPrg.Units;

public class Randomizer
{

    public static void RemoveWordFromList(string word)
    {
        FileOperation.WordList.Remove(word);
    }

    public static string GetRandomWord()
    {
        if (FileOperation.WordList.Count == 0)
        {
            throw new RandomizerException();
        }
        int maxRandom = FileOperation.WordList.Count - 1;
        int randomIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, maxRandom);
        return FileOperation.WordList[randomIndex];
    }
}

public class RandomizerException : Exception { }