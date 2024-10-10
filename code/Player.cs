namespace Kira;

public sealed class Player : Component
{
    private City city = new City();
    public City City => city;
}