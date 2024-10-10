namespace Kira;

public class City
{
    public string CityName { get; set; }
    public int CityAgeYears { get; set; }

    public CityStateStat CityState { get; set; }
}

public struct CityStateStat
{
    public long TimeStamp { get; set; }
    public float CityIncome { get; set; } // Detailed stats / breakdown of income
    public int Population { get; set; }
    public float PopGrowth { get; set; }
}