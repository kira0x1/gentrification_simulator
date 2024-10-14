namespace Kira;

using Utils;

public struct CitizenState
{
    public string firstName;
    public string lastName;
    public string fullName => $"{firstName} {lastName}";

    //TODO: Job class, Company, position, etc

    public bool isHomeless;
    public int age;

    public float heightCm;
    public float weightKg;

    public Color hairColor;

    public Model hairModel;
    public Model beardModel;
    public Model shirtModel;
    public Model pantsModel;
    public Model shoesModel;

    public Clothing hairClothes;
    public Clothing beardClothes;
    public Clothing shirtClothes;
    public Clothing pantsClothing;
    public Clothing shoesClothing;

    public ClothingContainer container;

    public bool hasBeard;
}