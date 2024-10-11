namespace Kira;

public sealed class Player : Component
{
    private CityManager CityManager { get; set; }

    protected override void OnAwake()
    {
        CityManager = Components.Get<CityManager>();
    }
}