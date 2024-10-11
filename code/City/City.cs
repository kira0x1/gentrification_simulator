namespace Kira;

public class City
{
    public string CityName { get; set; }
    public int CityAgeYears { get; set; }
    public CityStateStat CityState = new CityStateStat();

    public int Homelessness
    {
        get => CityState.homelessness;
        set => CityState.homelessness = value;
    }

    public int Population
    {
        get => CityState.population;
        set => CityState.population = value;
    }
}

public struct CityStateStat
{
    public long TimeStamp { get; set; }

    // Population
    public int population;
    public float popGrowth;
    public int homelessness;

    // Income
    public float CityIncome { get; set; } // TODO: Detailed stats / breakdown of income
    public float MedianIncome { get; set; }
    public float AverageIncome { get; set; }

    // Jobs
    // TODO: Job breakdown, office, educated workers, skilled labour, etc
    public int JobVacancies { get; set; }
    public int Unemployment { get; set; }
}