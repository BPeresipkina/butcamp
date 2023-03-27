using static System.Console; // чтобы не писать Console.
using static System.String; // чтобы не писать String.

public static class Infrastructure
{
    public static int[] CreatArray(this int size, int min = 0, int max = 10)
    {
        return Enumerable.Range(1, size)
                .Select(item => Random.Shared.Next(min, max))
                .ToArray();
    }

    public static int[] Show(this int[] array)
    {
        WriteLine($"[{Join(',', array)}]");
        return array;
    }
}