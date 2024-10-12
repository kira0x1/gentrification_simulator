namespace Kira;

using Utils;

public struct CitizenState
{
    public string firstName;
    public string lastName;
    public string fullName => $"{firstName} {lastName}";

    public bool isHomeless;
    public int age;

    public float heightCm;
    public float weightKg;

    public Color hairColor;
    public Model hairModel;
    public Model beardModel;
    public Model shirtModel;
    public Model pantsModel;

    public bool hasBeard;

    private static WeightedData CitizenColorWeights = new WeightedData(new[]
    {
        (110, 120, 800),
        (120, 140, 5100),
        (140, 160, 13500),
        (160, 180, 9200),
        (180, 190, 5000),
        (190, 190, 0)
    });

    public Dictionary<Color, (int Min, int Max, int Weight)> WeightedColors { get; set; }
}