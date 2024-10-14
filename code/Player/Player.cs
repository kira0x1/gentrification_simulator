namespace Kira;

public sealed class Player : Component
{
    public int Money { get; set; } = 50000;
    private CityManager CityManager { get; set; }

    protected override void OnAwake()
    {
        CityManager = Components.Get<CityManager>();
    }
}