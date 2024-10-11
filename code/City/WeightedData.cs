using System;

namespace Kira.Utils;

public readonly struct WeightedData
{
    public readonly (int Min, int Max, int Weight)[] Ranges;
    public readonly int TotalWeight;

    public WeightedData((int Min, int Max, int Weight)[] ranges)
    {
        Ranges = ranges;
        TotalWeight = Ranges.Sum(x => x.Weight);
    }

    private static readonly List<(Color Min, Color Max, int Weight)> ColorRanges = new List<(Color Min, Color Max, int Weight)>
    {
        (new Color(0), new Color(20), 10000),
        (new Color(60), new Color(80), 50000),
        (new Color(100), new Color(130), 10000),
        (new Color(190), new Color(200), 5000),
        (new Color(245), new Color(255), 1000)
    };

    public int GenerateRandom()
    {
        var rnd = new Random();

        var randomNum = rnd.Next(TotalWeight);

        foreach ((int min, int max, int weight) in Ranges)
        {
            if (randomNum < weight)
                return rnd.Next(min, max);
            randomNum -= weight;
        }

        return 0;
    }
}